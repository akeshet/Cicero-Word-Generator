// forms2.cpp : main project file.
#include "stdafx.h"
#include "Form1.h"
using namespace forms2;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	// Enabling Windows XP visual effects before any controls are created
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 
	
	System::Runtime::Remoting::Lifetime::LifetimeServices::LeaseTime = TimeSpan(1000, 0, 0, 0, 0);

	// Create the main window and run it
	Application::Run(gcnew Form1());
	return 0;
}
