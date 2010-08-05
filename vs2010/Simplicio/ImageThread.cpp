#include "stdafx.h"
//#include "CameraThread.h"
#include "form1.h"
//#include "CameraSettings.h"
//#include "sencam.h"
#include "ImageData.h"
#include "ImageThread.h"

namespace forms2{
	using namespace System;
	using namespace System::Threading;

	void ImageThread::init(Form1^ f){
		mainForm = f;
		processing=false;
		imageData = nullptr;
		buffers = nullptr;
		saveFile=true;
		addBuffersMainForm = gcnew DelegateBuffersIntImg(f,&Form1::addBuffers);
	}
	bool ImageThread::processImage(ImageData^ img, bool savefile,int binsize,int pixelsize){
		if (processing || img==nullptr) return true;
		imageData = img;
		saveFile = savefile;
		binSize=binsize;
		pixelSize=pixelsize;
		Thread^ imageThread = gcnew Thread(gcnew ThreadStart(this,&ImageThread::processNewImage));
		imageThread->Start();
		return false;
	}
	int max(int x,int y){return x>y?x:y;}
	int min(int x,int y){return x<y?x:y;}
	void ImageThread::processNewImage(){
		//creates new buffers and calculates values
		//report to Form1: buffers, numBuffers, [imageData--or write calculations to buffers]
		if (imageData==nullptr) return;
		processing = true;
		
		//allocate new buffers
		int layers = imageData->getLayers();
		numBuffers = layers;
		if (layers==3)
			numBuffers = 4;//create an extra buffer
		buffers=mainForm->getEmptyBuffers(numBuffers);
		int cols = imageData->getCols();//min(img->getCols(),pictureBox->Width);
		int rows = imageData->getRows()*imageData->getDoubler();//min((img->getRows())*(img->getDoubler()),pictureBox->Height);
		//MessageBox::Show(String::Format("rows: {0}",rows),"Box",MessageBoxButtons::OK);
			
		//find maximum values in each layer for normalization
		array<UInt16>^ maxVals = gcnew array<UInt16>(layers);
		for (int lay=0;lay<layers;lay++)
			maxVals[lay] = max(imageData->getMaxValue(lay),1);
		
		//draw the new data onto the new buffers
		int value,bufLay;
		SolidBrush^ brush = gcnew SolidBrush(Color::FromArgb(155,0,0));
		array<UInt32>^ counts = gcnew array<UInt32>(layers);//holds the pixel data from each layer
		UInt32 paddedNumerator, denominator, ratio;
		for (int r(0);r<rows;r+=binSize){
			for(int c(0);c<cols;c+=binSize){	
				//for each pixel:
				//read each layer and draw
				for (int lay(0);lay<layers;lay++){
					counts[lay] = imageData->getValue(r,c,lay,0);
					ratio = (255*counts[lay])/maxVals[lay];
					value = (int)ratio;
					if (value>255){ MessageBox::Show("Color value  > 255","Box",MessageBoxButtons::OK); value=255;}
					brush->Color = Color::FromArgb(value,value,value);
					bufLay = lay;
					if (layers==3) bufLay++;
					buffers[bufLay]->Graphics->FillRectangle(brush,Rectangle(pixelSize*c/binSize,pixelSize*r/binSize,pixelSize,pixelSize));
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
						buffers[0]->Graphics->FillRectangle(brush,Rectangle(pixelSize*c/binSize,pixelSize*r/binSize,pixelSize,pixelSize));
					}
				}
			}
		}
		//draw time, max value, and list-driven variables on each image:
		System::Drawing::Font^ font = gcnew System::Drawing::Font("Arial",12);
		LinkedList<String^>^ strList = gcnew LinkedList<String^>();//holds the strings to draw
		//define white box behind text
		int bw(200),bh(20);
		int x = pixelSize*cols/binSize-bw;
		int y = pixelSize*rows/binSize;
		//loop over buffers and draw text
		brush->Color = Color::White;
		for (int bufLay(0); bufLay<numBuffers;bufLay++){
			if (layers!=3 || bufLay!=0)//not a transmission image 
			{
				int lay = bufLay;//image data layer for max value
				if (layers==3) lay--;
				strList->AddLast(String::Format("Maximum Value: {0}",maxVals[lay]));
			}else 
			{//transmission image
				for each(Variable^ var in imageData->getSeqVars())
					strList->AddLast(String::Format("{0} = {1}", var->VariableName, var->VariableValue));
			}
			strList->AddFirst(imageData->getDateTimeString());
			buffers[bufLay]->Graphics->FillRectangle(brush,Rectangle(x,y,bw,bh*strList->Count));
			int strInd(0);
			for each(String^ str in strList){
				buffers[bufLay]->Graphics->DrawString(str,font,Brushes::Black, Point(x+15,y+bh*strInd) );
				strInd++;
			}
			strList->Clear();
		}
		//save to file
		//if (saveFile)
		//	imageData->saveFile();
		//send buffers to main window
		//if(callBack)
		array<Object^>^ parameters = gcnew array<Object^>(3);
		parameters[0] = buffers;
		parameters[1] = numBuffers;
		parameters[2] = imageData;
		mainForm->BeginInvoke(addBuffersMainForm, parameters);
		processing=false;
	}

}