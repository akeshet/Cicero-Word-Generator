#pragma once

namespace forms2{
	using namespace System;
	ref class ImageData;
	enum FileType {AIA, HDF};
	
	public ref class FileThread{
	public:
		FileThread(char p, FileType ft,ImageData^ img,bool split){
			init(p,ft,img,split);
		}
		~FileThread(){}
	
	private:
		void init(char p, FileType ft,ImageData^ img,bool split);

	};
}