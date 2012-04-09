function pllid = getpll22393configuration(xid, pllid)

%GETPLL22393CONFIGURATION  Reads the PLL configuration.
%  PLLID=GETPLL22393CONFIGURATION(XID,PLLID) reads the current PLL configuration
%  and sets the appropriate parameters in the given PLL object (PLLID).
%
%  Example:
%    xid = okfrontpanel()
%    pllid = okpll22393()
%    getpll22393configuration(xid, pllid)
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_GetPLL22393Configuration', xid.ptr, getobjptr(pllid));
