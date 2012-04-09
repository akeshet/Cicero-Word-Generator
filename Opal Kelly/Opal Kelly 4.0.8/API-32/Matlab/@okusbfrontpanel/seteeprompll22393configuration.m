function seteeprompll22393configuration(xid, pllid)

%SETEEPROMPLL22393CONFIGURATION  Stores a PLL configuration in EEPROM.
%  SETEEPROMPLL22393CONFIGURATION(XID,PLLID) stores the PLL configuration
%  from PLLID into the device EEPROM.
%
%  Example:
%    xid = okxem3001v2()
%    pllid = okpll22150()
%    geteeprompll22393configuration(xid, pllid)
%    ...change the pll config...
%    seteeprompll22393configuration(xid, pllid)
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_SetEepromPLL22393Configuration', xid.ptr, getobjptr(pllid));
