#include "stdafx.h"
#include "CameraThread.h"
#include "form1.h"
#include "CameraSettings.h"
#include "sencam.h"
#include "ImageData.h"
//#include "cam_types.h"

namespace forms2{
	using namespace System;
	using namespace System::IO;
	using namespace System::Threading;
	
	void CameraThread::init(Form1^ f, String^ path){
		mainForm = f;
		setLayersReadMainWindow = gcnew DelegateInt(f,&Form1::setLayersRead);
		loopFinishedMainWindow = gcnew DelegateVoid(f,&Form1::loopFinished);
		setImgMainWindow = gcnew DelegateImg(f,&Form1::addImageData);

		camSet = new CameraSettings();
		continueLock = gcnew String("");
		interruptLock = gcnew String("");
		filePath = String::Copy(path);
		continueImageLoop = false;
		interruptImageLoop = false;
		running = false;
		cameraInited = false;
		callBack = true;
		save=true;
		nextTime = NO_TIME;
		seqVars = gcnew LinkedList<Variable^>();
	}
	void CameraThread::setPath(String^ path){
		filePath = String::Copy(path);
	}
	void CameraThread::initCamera(){
		if (running){
			MessageBox::Show("Stop the image acquisition before re-initializing the camera.","Box",MessageBoxButtons::OK);
			return;
		}
		SET_BOARD(0);//selects the 0-th PCI board
		//initialize the camera using stored parameters
		int err = SET_INIT(1);
		if (err){
			//initialization returned an error
			MessageBox::Show( String::Concat("Error initializing camera. Error Code: ",err),"Box",MessageBoxButtons::OK);
			return;
		}
		err = STOP_COC(0);//prevents unintended acquisition
		if (err){
			MessageBox::Show( String::Concat("Error stopping camera operating code. Error Code: ",err),"Box",MessageBoxButtons::OK);
			return;
		}
		if (openCameraDialog()) return;
		//no errors:
		cameraInited = true;	
	}
	bool CameraThread::isRunning(){
		return running;
	}
	bool CameraThread::openCameraDialog()
	{//opens the camera settings dialogue box
		CLOSE_DIALOG_CAM();
		if(OPEN_DIALOG_CAM(NULL,0,NULL)){
			MessageBox::Show("Error opening camera settings dialogue box.","Box",MessageBoxButtons::OK);
			return true;
		}
		return false;

	}
	bool CameraThread::acquire(int layers, bool runLoop){
		//start a new thread to acquire images
		//return true if something prevents the form from starting
		if (running) return true;
		if (!cameraInited){
			MessageBox::Show("Initialize camera first","Box",MessageBoxButtons::OK);return true;}
		setContinue(runLoop);
		setInterrupt(false);
		
		Thread^ acquireThread = gcnew Thread(gcnew ParameterizedThreadStart(this,&CameraThread::takeImages));
		acquireThread->Start(layers);
		return false;
	}
	void CameraThread::stop(){
		setContinue(false);
	}
	void CameraThread::interrupt(bool callback){
		callBack=callback;
		setInterrupt(true);
		setContinue(false);
	}
	void CameraThread::closeCamera(){
		STOP_COC(0);
		LOCK_DIALOG_CAM(0);//neccessary?
		CLOSE_DIALOG_CAM();
		SET_INIT(0);
	}
	bool CameraThread::getContinue(){
		bool continueTemp=false;
		Monitor::Enter(continueLock);
		try{
			continueTemp = continueImageLoop;
		}
		finally{
			Monitor::Exit(continueLock);
		}
		return continueTemp;

	}
	void CameraThread::setContinue(bool c){
		Monitor::Enter(continueLock);
		try{
			continueImageLoop = c;
		}
		finally{
			Monitor::Exit(continueLock);
		}
	}
	bool CameraThread::getInterrupt(){
		bool interruptTemp=false;
		Monitor::Enter(interruptLock);
		try{
			interruptTemp = interruptImageLoop;
		}
		finally{
			Monitor::Exit(interruptLock);
		}
		return interruptTemp;
	}
	void CameraThread::setInterrupt(bool c){
		Monitor::Enter(interruptLock);
		try{
			interruptImageLoop = c;
		}
		finally{
			Monitor::Exit(interruptLock);
		}
	}
	void CameraThread::finishedRunning(){
		if (callBack)
			mainForm->BeginInvoke(loopFinishedMainWindow);
		LOCK_DIALOG_CAM(0);
		running = false;
	}
	void CameraThread::readLayers(int layersRead){
		array<Object^>^ parameters = gcnew array<Object^>(1);
		parameters[0] = layersRead;
		if (callBack)
			mainForm->BeginInvoke(setLayersReadMainWindow,parameters);
	}
	void CameraThread::gotImage(ImageData^ img){
		/*
		if (img!=nullptr)
			MessageBox::Show("Got image.","Box",MessageBoxButtons::OK);
		else
			MessageBox::Show("Got image: null","Box",MessageBoxButtons::OK);
		*/
		array<Object^>^ parameters = gcnew array<Object^>(1);
		parameters[0] = img;
		if(callBack)
			mainForm->BeginInvoke(setImgMainWindow,parameters);
	}
	void CameraThread::setSave(bool saveFiles){
		save = saveFiles;
	}

	void CameraThread::takeImages(Object^ layersObj)
	{
		const int layers = (int)layersObj;
		LOCK_DIALOG_CAM(1);
		running=true;
		const int rows = camSet->getRows();
		const int cols = camSet->getCols();
		const int dbl = (camSet->isDouble())? 2:1;
		
		//allocate memory for the images
		UInt16 *buf = (UInt16 *)malloc(2*dbl*cols*rows*layers);
		if (buf==0){
			MessageBox::Show("Buffer memory allocation failed.","Box",MessageBoxButtons::OK);
			finishedRunning();
		}
		STOP_COC(0);
		bool runErr=false;
		do{//loop for several images
			int layersRead = 0;		
			readLayers(layersRead);
			//loop over each layer of the current image
			for (int lay=0;lay<layers;lay++)
			{
				//STOP_COC(0);
				RUN_COC(4);//expose the CCD once
				int picstat;
				int errS;
				do{
					errS=GET_IMAGE_STATUS(&picstat);
					Thread::Sleep(0);
				}while (errS==0 && ((picstat&0x02)!=0 || (dbl==4 && (picstat&16) == 0)) && !getInterrupt());
				if (getInterrupt()) break;
				if (errS){
					MessageBox::Show("Error while waiting for image","Box",MessageBoxButtons::OK); break;}
				if((picstat&0x02)!=0){
					MessageBox::Show("No image","Box",MessageBoxButtons::OK); break;}
				//if (dbl==2 && (picstat&16) == 0)
				//	MessageBox::Show("One buffer empty","Box",MessageBoxButtons::OK);

				int width, height;
				GET_IMAGE_SIZE(&width,&height);
				if (width!=cols || height!=dbl*rows){//or if width>cols etc
					MessageBox::Show("Wrong dimensions in transfered image","Box",MessageBoxButtons::OK);break;}
				else{				
					int bufInd = lay*width*height;
					READ_IMAGE_12BIT(0,width,height,buf+bufInd);
					//STOP_COC(0);
					layersRead++;
					readLayers(layersRead);
				}
			}//end of layer for-loop
			if (layersRead==layers){//each layer read
				DateTime datetime = nextTime;
				if (datetime.Equals(NO_TIME))
					datetime = DateTime::Now;
				nextTime = NO_TIME;
				//copy the image data
				ImageData^ img = gcnew ImageData(buf,rows,cols,layers,(dbl==2),datetime); 
				img->setSeqVars(seqVars);
				seqVars->Clear();
				//save
				if (save) //saveImage(img);
					img->saveFile(filePath);
				//send the image to the main window
				gotImage(img);
				//saveImage(img);//(UInt16)rows*dbl,(UInt16)cols,(UInt16)layers,buf);
			}
			runErr = (layersRead!=layers);
		}while(!runErr && getContinue());
		free(buf);
		STOP_COC(0);
		///if (runErr) MessageBox::Show("Acquisition stopped ","Box",MessageBoxButtons::OK);
		finishedRunning();
	}
	void CameraThread::saveImage(ImageData^ img)//UInt16 rows, UInt16 cols, UInt16 layers, UInt16 *buf)
	{	//saves a 16-bit AIA file in little endian
		
		String^ extension =".aia";
		String^ basename = String::Format("{0}\\{1}",filePath,img->getDateTimeString());
		String^ filename = String::Copy(basename);
		
		//prevent overwrite by renaming
		for (int i=0; (i<1000) && File::Exists(String::Concat(filename,extension));i++){
			filename =String::Format("{0} Copy {1}",basename,i);// String::Concat(basename,i);
		}
		filename = String::Concat(filename,extension);
		if (File::Exists(filename)) return;

		FileStream^ fs = File::Open(filename,FileMode::Create);
		BinaryWriter^ bw = gcnew BinaryWriter(fs);
		try{
			Byte b0 = 0x41;
			Byte b1 = 0x49;
			Byte b2 = 0x41;
			bw->Write(b0);
			bw->Write(b1);
			bw->Write(b2);
			UInt16 intLength = 2;
			bw->Write(intLength);
			int rows = (img->getRows())*(img->getDoubler());
			int cols = img->getCols();
			int layers = img->getLayers();
			bw->Write((UInt16)rows);
			bw->Write((UInt16)cols);
			bw->Write((UInt16)layers);		
			for (int i=0;i<rows*cols*layers;i++)
				bw->Write(img->getValue(i));
		}
		finally{
			bw->Close();
			fs->Close();
		}
	}
	void CameraThread::setNextTime(DateTime nexttime){
		nextTime=nexttime;
	}
	void CameraThread::setSeqVars(LinkedList<Variable^>^ listvars){
		seqVars->Clear();
		for each(Variable^ var in listvars){
			Variable^ newvar = gcnew Variable();
			newvar->VariableValue = var->VariableValue;
			newvar->VariableName = var->VariableName;
			seqVars->AddLast(newvar);
		}
	}
}