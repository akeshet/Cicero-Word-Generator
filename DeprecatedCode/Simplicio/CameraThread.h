#pragma once

namespace forms2{

	ref class Form1;
	ref class ImageData;
	class CameraSettings;

	using namespace System;
	using namespace System::Collections::Generic;
	using namespace DataStructures;

	public ref class CameraThread
	{
	public:	
		CameraThread(Form1^ f, String^ path){init(f,path);}
		~CameraThread(){closeCamera();}

		void initCamera();
		bool acquire(int layers, bool runLoop);
		void stop();
		void interrupt(bool callback); 
		bool openCameraDialog();
		bool isRunning();
		void setPath(String^ path);
		void setSave(bool saveFiles);
		void setNextTime(DateTime nexttime);
		void setSeqVars(LinkedList<Variable^>^ vars);

	private:
		
		delegate void DelegateInt(int value);
		delegate void DelegateVoid();
		delegate void DelegateImg(ImageData^ img);

		DelegateInt^ setLayersReadMainWindow;
		DelegateVoid^ loopFinishedMainWindow;
		DelegateImg^ setImgMainWindow;

		Form1^ mainForm;	
		bool continueImageLoop;
		bool interruptImageLoop;
		bool callBack;
		String^ filePath;
		String^ continueLock;
		String^ interruptLock;

		bool running;
		bool cameraInited;
		bool save;
		CameraSettings* camSet;
		DateTime nextTime;
		LinkedList<Variable^>^ seqVars;

		static DateTime NO_TIME = DateTime(1,1,1);

		void init(Form1^ f, String^ path);
		void closeCamera();
		bool getContinue();
		void setContinue(bool c);
		bool getInterrupt();
		void setInterrupt(bool c);
		void takeImages(Object^ layersObj);
		void finishedRunning();
		void readLayers(int layersRead);
		void gotImage(ImageData^ img);
		void saveImage(ImageData^ img);//UInt16 rows, UInt16 cols, UInt16 lays, UInt16 *buf);
		
	};
}