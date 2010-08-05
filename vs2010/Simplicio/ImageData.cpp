#include "stdafx.h"
#include "ImageData.h"

namespace forms2{

	void ImageData::init(UInt16* b, int r, int c, int l, bool d,DateTime datetime){
		//copies b to imageBuffer
		rows = r; cols = c; layers = l; 
		dateTime = datetime;
		dbl = d ? 2 : 1;
		length = dbl*r*c*l;
		imageBuffer = gcnew array<UInt16>(length);
		for (int i=0; i<length;i++)
			imageBuffer[i] = b[i];
		saved = false;
		seqVars = gcnew LinkedList<Variable^>();
		
	}
	UInt16 ImageData::getValue(int i){
		return imageBuffer[i];
	}
	UInt16 ImageData::getValue(int r,int c,int l,int frame){
		if (dbl==1 && frame!=0)
			return 0;
		else if (frame !=0 && frame !=1) 
			return 0;
		int i = c + (r+frame*rows)*cols + l*dbl*rows*cols;
		return getValue(i);
	}
	UInt16 ImageData::getMaxValue(int layer, int frame){
		UInt16 max = 0;
		UInt16 v;
		for(int r=0;r<rows;r++)
			for(int c=0;c<cols;c++){
				v = getValue(r,c,layer,frame); 
				if (v > max)
					max = v;
			}
		return max;
	}
	UInt16 ImageData::getMaxValue(int layer){
		int max0 = getMaxValue(layer,0);
		int max = max0;
		if (dbl==2){
			int max1 = getMaxValue(layer,1);
			if (max1>max0)
				max = max1;
		}
		return max;
	}
	String^ ImageData::getDateTimeString(){
		DateTime dt = dateTime;
		return String::Format("{0:d4}-{1:d2}-{2:d2}-{3:d2};{4:d2};{5:d2}",dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second);
	}
	void ImageData::saveFile(String^ filePath)
	{	//saves a 16-bit AIA file in little endian
		
		String^ extension =".aia";
		String^ basename = String::Format("{0}\\{1}",filePath,getDateTimeString());
		String^ filename = String::Copy(basename);
		
		//prevent overwrite by renaming
		for (int i=0; (i<1000) && File::Exists(String::Concat(filename,extension));i++){
			filename =String::Format("{0} Copy {1}",basename,i);
		}
		filename = String::Concat(filename,extension);
		//if (File::Exists(filename)) return;

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
			int rows2 = (getRows())*(getDoubler());
			//int cols = getCols();
			//int layers = getLayers();
			bw->Write((UInt16)rows2);
			bw->Write((UInt16)cols);
			bw->Write((UInt16)layers);		
			for (int i=0;i<rows2*cols*layers;i++)
				bw->Write(getValue(i));
			//save sequence variables:
			for each(Variable^ var in seqVars){
				bw->Write(var->VariableName->ToCharArray());
				bw->Write((Byte)0);
				bw->Write((Double)var->VariableValue);
			}
			saved=true;
		}
		finally{
			bw->Close();
			fs->Close();
		}
	}
	void ImageData::setSeqVars(LinkedList<Variable^>^ vars){
		seqVars->Clear();
		for each(Variable^ var in vars){
			Variable^ newvar = gcnew Variable();
			newvar->VariableValue = var->VariableValue;
			newvar->VariableName = var->VariableName;
			seqVars->AddLast(newvar);
		}
	}
}