Matlab Sample API
-----------------

This Matlab library was developed as a sample to interface to the Opal Kelly
FrontPanel API.  It is currently NOT SUPPORTED and may require updates to
make it compatible with Matlab and the most recent API.

We include it here as the basis for further development.  It is a 
relatively straightforward matter to add new API methods to this
wrapper; Matlab supports external DLL calling through its own 
external interface.

As of 2011-05-30, this code works on Matlab R2010A installed on 
Windows XP, 32-bit.  Opal Kelly appreciates testing and contributions made
by Simeon Bamford.

To be compatible with the C language parser that is used in Matlabs external 
calling mechanism, the following changes may be required to the DLL:

---------------------------------------------------------------------------
1. Change the following lines (or similar):

   typedef void* okPLL22150_HANDLE;
   typedef void* okPLL22393_HANDLE;
   typedef void* okFrontPanel_HANDLE;
   
   to:
   
   typedef unsigned long okPLL22150_HANDLE;
   typedef unsigned long okPLL22393_HANDLE;
   typedef unsigned long okFrontPanel_HANDLE;
---------------------------------------------------------------------------
2. Comment out the lines:

   #ifndef FRONTPANELDLL_EXPORTS
      Bool okFrontPanelDLL_LoadLib(const char *libname);
      void okFrontPanelDLL_FreeLib(void);
   #endif
---------------------------------------------------------------------------
