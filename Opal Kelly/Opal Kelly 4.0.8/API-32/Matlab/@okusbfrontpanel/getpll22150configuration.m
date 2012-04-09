function pllid = getpll22150configuration(xid, pllid)

%GETPLL22150CONFIGURATION  Reads the PLL configuration.
%  PLLID=GETPLL22150CONFIGURATION(XID,PLLID) reads the current PLL configuration
%  and sets the appropriate parameters in the given PLL object (PLLID).
%
%  Example:
%    xid = okxem3001v2()
%    pllid = okpll22150()
%    getpll22150configuration(xid, pllid)
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_GetPLL22150Configuration', xid.ptr, getobjptr(pllid));
