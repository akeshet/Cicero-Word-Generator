#pragma once
namespace forms2{

	using namespace System::Drawing;
	ref class ImageData;
	public ref class ImageThread{
	public:
		ImageThread(Form1^ f){init(f);}
		~ImageThread(){}

		bool processImage(ImageData^ img, bool savefile,int binsize,int pixelsize);

	private:
		delegate void DelegateBuffersIntImg(array<BufferedGraphics^>^ newbuffers, int num, ImageData^ img);

		DelegateBuffersIntImg^ addBuffersMainForm;
		Form1^ mainForm;
		ImageData^ imageData;
		bool processing;
		bool saveFile;
		int binSize;
		int pixelSize;
		array<BufferedGraphics^>^ buffers;
		int numBuffers;

		void init(Form1^ f);
		void processNewImage();
	};
}