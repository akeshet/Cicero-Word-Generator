function s=has16bithostinterface(obj)

%HAS16BITHOSTINTERFACE  Returns 1 if the XEM has a 16-bit host interface
%  S=HAS16BITHOSTINTERFACE(OBJ) returns 1 if the device has a 16-bit host
%  interface.  All XEMs except for the XEM3001v1 have a 16-bit interface.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

s = calllib('okFrontPanel', 'okFrontPanel_Has16BitHostInterface', obj.ptr);
