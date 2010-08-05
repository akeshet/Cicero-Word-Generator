#include "stdafx.h"
#include "Form1.h"
//#include "sencam.h"
//#include "cam_types.h"
#include "CameraSettings.h"
#include "CameraThread.h"
#include "ImageThread.h"
#include "ImageData.h"
#include "SimplicioServer.h"
//#using "DataStructures.dll"
//using namespace System;
//using namespace System::IO;
//using namespace System::Threading;

namespace forms2{
	void Form1::InitProgram()
	{	//list with "none" then add times
		//cameraInited = false;
		//running = false;
		//continueImageLoop = false;
		//interruptImageLoop = false;
		pathLink->Text = folderBrowserDialog->SelectedPath;
		formatListBox->SelectedIndex = 0;
		//prevListBox->SelectedIndex = 0;//seems to trigger an action event
		pictureBox->Paint += gcnew System::Windows::Forms::PaintEventHandler( this, &Form1::pictureBox_Paint);
		context = BufferedGraphicsManager::Current;
		context->MaximumBuffer = System::Drawing::Size( this->Width + 1, this->Height + 1 );
	  //  imgBuffer = context->Allocate( pictureBox->CreateGraphics(), Rectangle(0,0,pictureBox->Width,pictureBox->Height) );
		imgBuffer = nullptr;
		//upcomingListVars = nullptr;
		//bufferArray = gcnew array<BufferedGraphics^>(NUM_BUFFERS); 
		buffers = gcnew array<array<BufferedGraphics^>^>(IMAGE_HISTORY_LENGTH);
		layerHistory = gcnew array<int>(IMAGE_HISTORY_LENGTH);
		imgDataArray = gcnew array<ImageData^>(IMAGE_HISTORY_LENGTH);
		/*
		for (int i=0;i<NUM_BUFFERS;i++)
			bufferArray[i] = context->Allocate( pictureBox->CreateGraphics(), Rectangle(0,0,pictureBox->Width,pictureBox->Height) );
		*/
		//imgDataArray = gcnew array<ImageData^>(IMAGE_HISTORY_LENGTH);
		for (int i=0;i<IMAGE_HISTORY_LENGTH;i++){
			buffers[i] = nullptr;
			layerHistory[i]=0;//zero layers
			imgDataArray[i] = nullptr;
		}
		imgDataIndex = -1;
		displayedImgInd = 0;
		displayedLayer = 0;
		//imgData = nullptr;
		//imgDataStack = gcnew Stack(10);
		server = gcnew SimplicioServer(this);
		camThread = gcnew CameraThread(this,pathLink->Text);
		camThread->initCamera();
		imgThread = gcnew ImageThread(this);
	}
	array<BufferedGraphics^>^ Form1::getEmptyBuffers(int numbuffers){
		if (numbuffers<1) return nullptr;
		array<BufferedGraphics^>^ newbuffers = gcnew array<BufferedGraphics^>(numbuffers);
		for (int i=0;i<numbuffers;i++){		
			newbuffers[i] = context->Allocate(pictureBox->CreateGraphics(), Rectangle(0,0,pictureBox->Width,pictureBox->Height));
			newbuffers[i]->Graphics->FillRectangle(Brushes::Black,Rectangle(0,0,pictureBox->Width,pictureBox->Height));
		}
		return newbuffers;
	}
	/*
	int min(int x,int y){
		return (x<y)? x : y;
	}
	int max(int x,int y){
		return (x>y)? x : y;
	}
	*/
	void Form1::setSaveData(bool savedata){
		camThread->setSave(savedata);
	}
	void Form1::addImageData(ImageData^ img){
	//	Thread^ imgThread = gcnew Thread(gcnew ParameterizedThreadStart(this,&Form1::processNewImage));
	//	imgThread->Start(img);
		imgThread->processImage(img,saveCheckBox->Checked,(int)zoomBox->Value,(int)pixelSizeBox->Value);
	}
	void Form1::addBuffers(array<BufferedGraphics^>^ newbuffers, int numbuffers,ImageData^ img){
		//called by imageThread after it processes a new image 
		if (newbuffers==nullptr || numbuffers<1) return;
		imgDataIndex++;
		if (imgDataIndex == IMAGE_HISTORY_LENGTH) 
			imgDataIndex = 0;
		buffers[imgDataIndex]=newbuffers;
		layerHistory[imgDataIndex]=numbuffers;
		//if(!img->getSaved())
		imgDataArray[imgDataIndex]=img;
		setDisplayImage(0,0);
	}
	/*
	void Form1::processNewImage(Object^ imgObj){
		ImageData^ img = (ImageData^)imgObj;
		if (img==nullptr) return;
		
		//set the index in buffers to store the new image buffers
		imgDataIndex++;
		if (imgDataIndex == IMAGE_HISTORY_LENGTH) 
			imgDataIndex = 0;
		//imgDataArray[imgDataIndex]=img;
		//drawBuffers(0);//optional
		
		//allocate new buffers
		int layers = img->getLayers();
		int length = layers;
		if (layers==3)
			length = 4;//create an extra buffer
		buffers[imgDataIndex]=gcnew array<BufferedGraphics^>(length);
		layerHistory[imgDataIndex] = length;
		int cols = img->getCols();//min(img->getCols(),pictureBox->Width);
		int rows = img->getRows()*img->getDoubler();//min((img->getRows())*(img->getDoubler()),pictureBox->Height);
		//MessageBox::Show(String::Format("rows: {0}",rows),"Box",MessageBoxButtons::OK);
		for (int i=0;i<length;i++){		
			buffers[imgDataIndex][i] = context->Allocate(pictureBox->CreateGraphics(), Rectangle(0,0,pictureBox->Width,pictureBox->Height));
			buffers[imgDataIndex][i]->Graphics->FillRectangle(Brushes::Black,Rectangle(0,0,pictureBox->Width,pictureBox->Height));
		}
		
		//find maximum values in each layer for normalization
		array<UInt16>^ maxVals = gcnew array<UInt16>(layers);
		for (int lay=0;lay<layers;lay++)
			maxVals[lay] = max(img->getMaxValue(lay),1);
		
		//draw the new data onto the new buffers
		int value,bufLay;
		SolidBrush^ brush = gcnew SolidBrush(Color::FromArgb(155,0,0));
		int binSize=(int)zoomBox->Value;	
		int pixelSize = (int)pixelSizeBox->Value;
		array<UInt32>^ counts = gcnew array<UInt32>(layers);//holds the pixel data from each layer
		UInt32 paddedNumerator, denominator, ratio;
		for (int r(0);r<rows;r+=binSize){
			for(int c(0);c<cols;c+=binSize){	
				//for each pixel:
				//read each layer and draw
				for (int lay(0);lay<layers;lay++){
					counts[lay] = img->getValue(r,c,lay,0);
					ratio = (255*counts[lay])/maxVals[lay];
					value = (int)ratio;
					if (value>255){ MessageBox::Show("Color value  > 255","Box",MessageBoxButtons::OK); value=255;}
					brush->Color = Color::FromArgb(value,value,value);
					bufLay = lay;
					if (layers==3) bufLay++;
					buffers[imgDataIndex][bufLay]->Graphics->FillRectangle(brush,Rectangle(pixelSize*c/binSize,pixelSize*r/binSize,pixelSize,pixelSize));
				}
				if (layers==3)
				{	
					if ((counts[0] > counts[2])&&(counts[1]>counts[2])){
						//draw transmission image	
						paddedNumerator = (counts[0]-counts[2])<<16;
						denominator = counts[1]-counts[2];
						ratio =paddedNumerator/denominator;
						//prevent ratio from exceeding 2*padding i.e. pwa twice as bright as pwoa
						if (ratio >= (2<<16))
							ratio = (2<<16) - 1;
						ratio = ratio>>9;//bring ratio into the range [0,255]
						value = (int)ratio;
						brush->Color = Color::FromArgb(value,value,value);
						buffers[imgDataIndex][0]->Graphics->FillRectangle(brush,Rectangle(pixelSize*c/binSize,pixelSize*r/binSize,pixelSize,pixelSize));
					}
				}
			}
		}
		//draw time and max value on each image
		System::Drawing::Font^ font = gcnew System::Drawing::Font("Arial",12);
		String^ str1 = img->getDateTime().ToString();
		String^ str2;
		int bw(170),bh(50);
		int x = min(pixelSize*cols/binSize + 20,pictureBox->Width-bw);
		int y = min(pixelSize*rows/binSize+30,pictureBox->Height - bh);
		brush->Color = Color::White;
		int bufLayers=layers;
		if (layers==3) bufLayers=4;
		for (int bufLay(0); bufLay<bufLayers;bufLay++){
			//draw text area and date/time
			buffers[imgDataIndex][bufLay]->Graphics->FillRectangle(brush,Rectangle(x,y,bw,bh));
			buffers[imgDataIndex][bufLay]->Graphics->DrawString(str1,font,Brushes::Black, Point(x,y) );
			//draw max values
			int lay = bufLay;
			if (layers==3) lay--;
			if (layers!=3 || bufLay!=0)//not a transmission image 
			{	
				str2 = String::Format("Maximum Value: {0}",maxVals[lay]);
				buffers[imgDataIndex][bufLay]->Graphics->DrawString(str2,font,Brushes::Black, Point(x,y+25) );
			}
		}
		//save to file
		if (saveCheckBox->Checked)
			img->saveFile();
		//display the new image
		setDisplayImage(0,0);
	}
	*/
	void Form1::saveImage(int stepsBack){
		int bInd = getHistoryIndex(stepsBack);
		if (bInd<0) return;
		if (imgDataArray[bInd]==nullptr) return;
		imgDataArray[bInd]->saveFile(pathLink->Text);
		setDisplayImage(displayedImgInd,displayedLayer);//updates the save button
	}
	int Form1::getHistoryIndex(int stepsBack){
		if (stepsBack < 0 || stepsBack >= IMAGE_HISTORY_LENGTH)
			return -1;
		int bInd = imgDataIndex - stepsBack;
		if (bInd<0)
			bInd+=IMAGE_HISTORY_LENGTH;
		//check if the index is valid
		if (bInd<0 || bInd >= IMAGE_HISTORY_LENGTH){
		//	MessageBox::Show("Image index out of bounds.","Box",MessageBoxButtons::OK);
			return -2;
		}
		return bInd;
	}
	void Form1::setDisplayImage(int stepsBack, int layer)
	{//sets one of the stored image buffers as the image to display
		
		if (stepsBack < 0 || stepsBack >= IMAGE_HISTORY_LENGTH){
		//	MessageBox::Show(String::Format("Image history index out of bounds: steps back = {0}",stepsBack),"Box",MessageBoxButtons::OK);
			return;
		}
		//set the index of the image to view
		int bInd = imgDataIndex - stepsBack;
		if (bInd<0)
			bInd+=IMAGE_HISTORY_LENGTH;
		
		//check if the index is valid
		if (bInd<0 || bInd >= IMAGE_HISTORY_LENGTH){
		//	MessageBox::Show("Image index out of bounds.","Box",MessageBoxButtons::OK);
		}
		else if(buffers[bInd]==nullptr){
		//	MessageBox::Show("No data stored for that index.","Box",MessageBoxButtons::OK);
		}
		else if(layer < 0 || layer >= layerHistory[bInd]){
		//	MessageBox::Show("No data stored for that layer.","Box",MessageBoxButtons::OK);
		}
		else
		{//display the image		
			imgBuffer = buffers[bInd][layer];
			if (frameListBox->SelectedIndex != layer)
				frameListBox->SelectedIndex = layer;
			if (prevListBox->SelectedIndex != stepsBack)
				prevListBox->SelectedIndex = stepsBack;
			displayedImgInd = stepsBack;
			displayedLayer = layer;
			//update saveButton
			if (imgDataArray[bInd]->getSaved()){
				saveButton->Text = "Re-Save";
				//saveButton->Enabled=false;
			}else{
				saveButton->Text = "Save Image Data";
				//saveButton->Enabled=true;
			}

			pictureBox->Refresh();
			return;
		}
		//a new image was not displayed
		if (frameListBox->SelectedIndex != displayedLayer)
			frameListBox->SelectedIndex = displayedLayer;
		if (prevListBox->SelectedIndex != displayedImgInd)
			prevListBox->SelectedIndex = displayedImgInd;
	}
	void Form1::selectPreviousImage(int stepsBack){
		//selects a previous image. tries to use the current layer index.
		if (stepsBack < 0 || stepsBack >= IMAGE_HISTORY_LENGTH)
			return;
		int bufLayer=frameListBox->SelectedIndex;
		int bInd = imgDataIndex - stepsBack;
		if (bInd<0)
			bInd+=IMAGE_HISTORY_LENGTH;
		//check if the index is valid
		//if (bInd<0 || bInd >= IMAGE_HISTORY_LENGTH){
		//	MessageBox::Show("Image index out of bounds.","Box",MessageBoxButtons::OK);
		//}
		//else if(buffers[bInd]==nullptr){
		//	MessageBox::Show("No data stored for that index.","Box",MessageBoxButtons::OK);
		//}
		//else

		if (bInd>=0 && bInd < IMAGE_HISTORY_LENGTH){
			//bInd is ok. pick a layer. 
			if(bufLayer < 0 || bufLayer >= layerHistory[bInd]){
				//currently selected layer is out of bounds 
				bufLayer=0;
				frameListBox->SelectedIndex = bufLayer;
			}
		}
		setDisplayImage(stepsBack,bufLayer);
	}
	/*
	void Form1::setDisplayImage(int bInd){
		imgBuffer = bufferArray[bInd];
		pictureBox->Refresh();
	}
	*/
/*
	void Form1::drawBuffers(int stepsBack){
		//displays one of the stored images
		//stepsBack = 0 means the most recent image
		int i = imgDataIndex - stepsBack;//index to imgDataArray
		if (i<0)
			i+=IMAGE_HISTORY_LENGTH;
		
		if (i<0 || i >= IMAGE_HISTORY_LENGTH)
			MessageBox::Show("Image index out of bounds.","Box",MessageBoxButtons::OK);
		else if(imgDataArray[i]==nullptr)
			MessageBox::Show("No data stored for that index.","Box",MessageBoxButtons::OK);
		else 
		{	//Image data exists at this index
			
			//update list  box
			//prevListBox->SelectedIndex = stepsBack;
			
			//draw buffers
			ImageData^ imgData = imgDataArray[i];
			const int rows = imgData->getRows()*imgData->getDoubler();//combine frames
			const int cols = imgData->getCols();
			//MessageBox::Show(imgData->getDoubler().ToString(),"Box",MessageBoxButtons::OK);
			int layers = imgData->getLayers();
			if (layers !=3) 
				layers =1;
			//draw 
			//if layer = 1, draw one buffer at 0
			//if layer = 3, draw four (or eight) buffers
			//	order: 0,1,2,.. = transA, pwaA, pwoaA, dfA...(B)
			//int lay = 0;
			//int frame = 0;
			//int max = (int)imgData->getMaxValue(lay,frame);
			int value;
			SolidBrush^ brush = gcnew SolidBrush(Color::FromArgb(155,0,0));
			int binSize=3;	
			if (layers==1){
				int max = (int)imgData->getMaxValue(0);
				//clear buffer before drawing
				bufferArray[0]->Graphics->FillRectangle(Brushes::Black,Rectangle(0,0,pictureBox->Width,pictureBox->Height));
				for (int r=0;r<rows;r+=binSize)
					for (int c=0;c<cols;c+=binSize){
						value = (int)imgData->getValue(r,c,0,0);
						value = 255*value/max;
						//if (value>255) value=255;
						brush->Color = Color::FromArgb(value,value,value);
						bufferArray[0]->Graphics->FillRectangle(brush,Rectangle(c/binSize,r/binSize,1,1));
					}
				//imgBuffer = bufferArray[0];
			}
			else{
				//three layers
				//find max values
				array<UInt16>^ max = gcnew array<UInt16>(3);
				for (int lay=0;lay<layers;lay++){	
					UInt16 m = imgData->getMaxValue(lay);
					max[lay] = (m>0)? m : 1; 
				}
				//clear buffers before drawing
				for (int bInd=0;bInd<4;bInd++)
					bufferArray[bInd]->Graphics->FillRectangle(Brushes::Black,Rectangle(0,0,pictureBox->Width,pictureBox->Height));
				//draw CCD images and transmission image
				array<UInt32>^ counts = gcnew array<UInt32>(layers);//holds the pixel data from each layer
				//double tNum, tDenom, tVal;
				UInt32 paddedNumerator, denominator, ratio;
				for (int r=0;r<rows;r+=binSize)
					for (int c=0;c<cols;c+=binSize){
						//draw this pixel onto CCD images
						for (int lay=0;lay<layers;lay++){
							counts[lay] = imgData->getValue(r,c,lay,0);
							ratio = (255*counts[lay])/max[lay];
							value = (int)ratio;
							//if (value>255) value=255;
							brush->Color = Color::FromArgb(value,value,value);
							bufferArray[lay+1]->Graphics->FillRectangle(brush,Rectangle(c,r,1,1));
						}
						//draw transmission image
						//tNum = (double)(counts[0]-counts[2]);//pwa-df
						//tDenom = (double)(counts[1]-counts[2]);//pwoa-df
						if ((counts[0] > counts[2])&&(counts[1]>counts[2])){
							paddedNumerator = (counts[0]-counts[2])<<16;
							denominator = counts[1]-counts[2];
							ratio =paddedNumerator/denominator;
							//prevent ratio from exceeding 2*padding i.e. pwa twice as bright as pwoa
							if (ratio >= (2<<16))
								ratio = (2<<16) - 1;
							ratio = ratio>>9;//bring ratio into the range [0,255]
							value = (int)ratio;
							brush->Color = Color::FromArgb(value,value,value);
							bufferArray[0]->Graphics->FillRectangle(brush,Rectangle(c/binSize,r/binSize,1,1));
						}else{
							//brush->Color = Color::FromArgb(200,0,0);
							//bufferArray[0]->Graphics->FillRectangle(brush,Rectangle(c,r,1,1));
						}

						
					//	bool badpixel=(tNum<0 || tDenom<0);					
					//	if(!badpixel){
					//		tVal = tNum/ tDenom;
					//		tVal*=200;
					//		value = (int)tVal;
					//		if (value>255)
					//			value = 255;
					//		if (value<0)
					//			value =0;
					//		brush->Color = Color::FromArgb(value,value,value);
					//	}
					//	else
					//		brush->Color = Color::FromArgb(200,0,0);
					//	
							
					}
				//imgBuffer = bufferArray[0];
			}
			setDisplayImage(0);
			//refresh
			//pictureBox->Refresh();	
		}
	}
	*/
	void Form1::pictureBox_Paint(Object^ /*sender*/, System::Windows::Forms::PaintEventArgs^ e )
	{
		if (imgBuffer!=nullptr)
			imgBuffer->Render(e->Graphics);
	}

	void Form1::EndProgram()
	{
		interrupt(false);
		//delete camThread;
		//STOP_COC(0);
		//LOCK_DIALOG_CAM(0);//neccessary?
		//CLOSE_DIALOG_CAM();
		//SET_INIT(0);
	}
	void Form1::changePath()
	{
		//if (camThread->isRunning()){
		//	MessageBox::Show("Stop the image acquisition before changing the path.","Box",MessageBoxButtons::OK);
		//	return;
		//}
		folderBrowserDialog->SelectedPath = pathLink->Text;
		System::Windows::Forms::DialogResult result = folderBrowserDialog->ShowDialog();
		if ( result == System::Windows::Forms::DialogResult::OK ){
			pathLink->Text = folderBrowserDialog->SelectedPath;
			camThread->setPath(pathLink->Text);
		}
	}
	
	void Form1::initCamera()
	{
		camThread->initCamera();
		/*
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
		*/
	}
	
	bool Form1::openCameraDialog()
	{//opens the camera settings dialogue box
		/*
		if(OPEN_DIALOG_CAM(NULL,0,NULL)){
			MessageBox::Show("Error opening camera settings dialogue box.","Box",MessageBoxButtons::OK);
			return true;
		}
		return false;
		*/
		return camThread->openCameraDialog();
	}
	void Form1::stopImageLoop()
	{
		camThread->stop();
	}
	void Form1::interrupt(bool callback)
	{
		camThread->interrupt(callback);
	}
	
	void Form1::acquire(bool runLoop)
	{
		bool err = camThread->acquire((int)layersBox->Value,runLoop);
		if (!err){
			runIndicator->Checked = true;
			layersBox->Enabled = false;
			imageProgressBar->Maximum = (int)layersBox->Value;
			imageProgressBar->Value = 0;
		}
		/*
		if (running) return;
		if (!cameraInited){
			MessageBox::Show("Initialize camera first","Box",MessageBoxButtons::OK);return;}
		LOCK_DIALOG_CAM(1);
		running=true;
		continueImageLoop=runLoop;
		interruptImageLoop = false;
		
		Thread^ acquireThread = gcnew Thread(gcnew ParameterizedThreadStart(this,&Form1::takeImage));
		acquireThread->Start(runLoop);

		//takeImage(runLoop);
		*/
	}
	
	/*
	delegate void ProgressDelegate(int value);

	void Form1::setProgressValue(int value){
		//if (imageProgressBar->InvokeRequired()== false)
		//	imageProgressBar->Value = value;
		//else{//create a delegate and call again
			ProgressDelegate^ progressDelegate = gcnew ProgressDelegate(this,&Form1::setProgressValueDirectly);
			array<Object^>^ parameters = gcnew array<Object^>(1);
			parameters[0] = value;
			this->BeginInvoke(progressDelegate, parameters);
		//}

	}
	void Form1::setProgressValueDirectly(int value){
			imageProgressBar->Value = value;
	}
	*/
	void Form1::setLayersRead(int value){
			imageProgressBar->Value = value;
	}
	void Form1::loopFinished(){
		layersBox->Enabled = true;
		runIndicator->Checked = false;
	}
	void Form1::setNextTime(DateTime nextTime){
		camThread->setNextTime(nextTime);
	}
	void Form1::sequenceStarted(LinkedList<Variable^>^ listvars){
		//upcomingListVars = listvars;
		camThread->setSeqVars(listvars);
		acquire(false);
	}
	/*
	void Form1::takeImage(Object^ runloop)
	{	//takes an images
		bool runLoop = (bool)runloop;
		const int layers =(int)layersBox->Value;
		//this.BeginInvokve(delegate//layersBox->Enabled = false);
		//imageProgressBar->Maximum = layers;
		CameraSettings camSet;
		const int rows = camSet.getRows();
		const int cols = camSet.getCols();
		const int mode = camSet.getSetting(CameraSettings::MODE);
		//const int typ = mode & 0x00FF;//byte 0
		const int submode = (mode & 0xFF0000)>>16;//byte 2
		const int dbl = (submode==QE_DOUBLE)? 2:1;
		//const int rows2 = dbl*rows;
		//MessageBox::Show(dbl.ToString(),"Box",MessageBoxButtons::OK);
		
		UInt16 *buf = (UInt16 *)malloc(2*dbl*cols*rows*layers);
		if (buf==0){
			MessageBox::Show("Buffer memory allocation failed.","Box",MessageBoxButtons::OK);
			return;
		}
		bool runErr;
		do{//loop for several images
			int layersRead = 0;		
			//imageProgressBar->Value = layersRead;//needs to use invoke
			
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
				}while (errS==0 && ((picstat&0x02)!=0 || (dbl==4 && (picstat&16) == 0)) && !interruptImageLoop);
				if (interruptImageLoop) break;
				if (errS){
					MessageBox::Show("Error while waiting for image","Box",MessageBoxButtons::OK); break;}
				if((picstat&0x02)!=0){
					MessageBox::Show("No image","Box",MessageBoxButtons::OK); break;}
				//if (dbl==2 && (picstat&16) == 0)
				//	MessageBox::Show("One buffer empty","Box",MessageBoxButtons::OK);
			//	imageProgressBar->Value++;

				int width, height;
				GET_IMAGE_SIZE(&width,&height);
				if (width!=cols || height!=dbl*rows){
					MessageBox::Show("Wrong dimensions in transfered image","Box",MessageBoxButtons::OK);break;}
				else{				
					int bufInd = lay*width*height;
					READ_IMAGE_12BIT(0,width,height,buf+bufInd);
					//STOP_COC(0);
					layersRead++;
					//imageProgressBar->Value = layersRead;
					setProgressValue(layersRead);
				}
			}//end of layer for-loop
			if (layersRead==layers)
					//add: split double images, run save in new thread
					saveImage((UInt16)rows*dbl,(UInt16)cols,(UInt16)layers,buf);
			runErr = (layersRead!=layers);
		}while(!runErr && continueImageLoop);
		STOP_COC(0);
		free(buf);
		running=false;
		//if (runErr) MessageBox::Show("Error","Box",MessageBoxButtons::OK);
		//imageProgressBar->Value = 0;
		layersBox->Enabled = true;//!!invoke
		runIndicator->Checked = false;//!!!invoke
		LOCK_DIALOG_CAM(0);
	}
	*/
	/*
	void Form1::saveImage(UInt16 rows, UInt16 cols, UInt16 layers, UInt16 *buf)
	{	//saves a 16-bit AIA file in little endian
		//MessageBox::Show("saveImage reached","Box",MessageBoxButtons::OK);
		String^ extension = String::Concat(".",formatListBox->SelectedItem);//".aia";
		String^ path = pathLink->Text;		
		String^ basename = String::Concat(path,"\\data");//"data"
		//MessageBox::Show(basename,"Box",MessageBoxButtons::OK);
		String^ filename = String::Copy(basename);
		for (int i=0; (i<1000) && File::Exists(String::Concat(filename,extension));i++){
			filename = String::Concat(basename,i);
		}
		if (File::Exists(String::Concat(filename,extension))) return;
		FileStream^ fs = File::Open(String::Concat(filename,extension),FileMode::Create);
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
			bw->Write(rows);
			bw->Write(cols);
			bw->Write(layers);		
			for (int i=0;i<rows*cols*layers;i++)
				bw->Write(buf[i]);
		}
		finally{
			bw->Close();
			fs->Close();
		}
	}
	*/
}