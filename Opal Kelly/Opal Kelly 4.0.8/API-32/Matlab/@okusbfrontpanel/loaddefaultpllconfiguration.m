function epval = loaddefaultpllconfiguration(obj)

%LOADDEFAULTPLLCONFIGURATION  Configures the PLL from the EEPROM settings.
%  LOADDEFAULTPLLCONFIGURATION(OBJ) configures the on-board PLL according
%  to the settings previously stored in EEPROM.  This is equivalent to 
%  consecutive calls to GETEEPROMPLLxxxCONFIGURATION and 
%  SETPLLxxCONFIGURATION.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_LoadDefaultPLLConfiguration', obj.ptr);
