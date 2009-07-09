#ifndef CAMERA_SETTINGS
#define CAMERA_SETTINGS

/*
		int mode;
		int trig;
		int roix1; 
		int roix2; 
		int roiy1; 
		int roiy2;
		int hbin;
		int vbin;
		char table[300];
		GET_SETTINGS( &mode, &trig, &roix1, &roix2, &roiy1, &roiy2, &hbin, &vbin, (char**)table);
		int cols = 32*(roix2-roix1+1);
		int rows = 32*(roiy2-roiy1+1);
		*/

namespace forms2{
	public class CameraSettings{
	public:
		static const int MODE = 1;
		static const int TRIG = 1<<1;
		static const int ROIX1 = 1<<2;
		static const int ROIX2 = 1<<3;
		static const int ROIY1 = 1<<4;
		static const int ROIY2 = 1<<5;
		static const int HBIN = 1<<6;
		static const int VBIN = 1<<7;
	//	static const int TABLE = 1<<8;
		static const int NULL_SETTING = -999;

		CameraSettings(void);
		int getSetting(int settingID);
		int getRows();
		int getCols();
		int getHBin(){getSettings();return hbin;}
		int getVBin(){getSettings();return vbin;}
		bool isDouble();
		void update();

	protected:
		int mode;
		int trig;
		int roix1; 
		int roix2; 
		int roiy1; 
		int roiy2;
		int hbin;
		int vbin;
		char table[300];

		void getSettings();
	};
}

#endif