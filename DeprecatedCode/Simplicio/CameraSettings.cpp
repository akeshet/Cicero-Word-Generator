#include "stdafx.h"
#include "CameraSettings.h"
#include "sencam.h"
#include "cam_types.h"

namespace forms2{
	CameraSettings::CameraSettings(void)
	{
		mode = NULL_SETTING;
		trig = NULL_SETTING;
		roix1 = NULL_SETTING;
		roix2 = NULL_SETTING;
		roiy1 = NULL_SETTING;
		roiy2 = NULL_SETTING;
		hbin = NULL_SETTING;
		vbin = NULL_SETTING;
	}

	int CameraSettings::getSetting(int settingID)
	{
		getSettings();
		switch(settingID){
		case MODE: return mode; break;
		case TRIG: return trig; break;
		case ROIX1: return roix1; break;
		case ROIX2: return roix2; break;
		case ROIY1: return roiy1; break;
		case ROIY2: return roiy2; break;
		case HBIN: return hbin; break;
		case VBIN: return vbin; break;
		default: return NULL_SETTING;
		}
	}

	int CameraSettings::getCols()
	{
		getSettings();
		return 32*(roix2-roix1+1)/hbin;
	}
	
	int CameraSettings::getRows()
	{ 
		getSettings();
		int rows = 	32*(roiy2-roiy1+1);
		if (roiy2==33)
			rows-=16;
		return rows/vbin;
	}
	bool CameraSettings::isDouble(){
		getSettings();
		//const int typ = mode & 0x00FF;//byte 0
		const int submode = (mode & 0xFF0000)>>16;//byte 2
		//int dbl = (submode==QE_DOUBLE)? 2:1;
		return (submode==QE_DOUBLE);
	}

	void CameraSettings::update()
	{
		getSettings();
	}

	void CameraSettings::getSettings()
	{
		GET_SETTINGS( &mode, &trig, &roix1, &roix2, &roiy1, &roiy2, &hbin, &vbin, (char**)table);
	}

}