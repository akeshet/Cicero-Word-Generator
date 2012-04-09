function setpll22393configuration(xid, pllid)

%SETPLL22393CONFIGURATION  Configure the on-board PLL.
%  SETPLL22393CONFIGURATION(XID,PLLID) programs the on-board PLL with
%  the parameters in PLLID.  The new settings will take effect
%  immediately.
%
%  Example:
%    xid = okxem3001v2()
%    pllid = okpll22150()
%    geteeprompll22393configuration(xid, pllid)
%    setpll22393configuration(xid, pllid)
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_SetPLL22393Configuration', xid.ptr, getobjptr(pllid));
