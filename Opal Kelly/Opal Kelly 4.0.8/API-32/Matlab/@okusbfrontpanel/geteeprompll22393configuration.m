function pllid=geteeprompll22393configuration(xid, pllid)

%GETEEPROMPLL22393CONFIGURATION  Reads the PLL configuration from EEPROM.
%  PLLID=GETEEPROMPLL22393CONFIGURATION(XID,PLLID) reads the current PLL
%  configuration stored in EEPROM and sets the appropriate parameters in
%  the given PLL object (PLLID).
%
%  Example:
%    xid = okxem3010()
%    pllid = okpll22393()
%    geteeprompll22393configuration(xid, pllid)
%    setpll22393configuration(xid, pllid)
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_GetEepromPLL22393Configuration', xid.ptr, getobjptr(pllid));
