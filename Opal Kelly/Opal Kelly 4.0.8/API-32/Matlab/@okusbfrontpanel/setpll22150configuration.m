function setpll22150configuration(xid, pllid)

%SETPLL22150CONFIGURATION  Configure the on-board PLL.
%  SETPLL22150CONFIGURATION(XID,PLLID) programs the on-board PLL with
%  the parameters in PLLID.  The new settings will take effect
%  immediately.
%
%  Example:
%    xid = okxem3001v2()
%    pllid = okpll22150()
%    geteeprompll22150configuration(xid, pllid)
%    setpll22150configuration(xid, pllid)
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_SetPLL22150Configuration', xid.ptr, getobjptr(pllid));
