function setdeviceid(obj, stringID)

%SETDEVICEID  Set the device ID.
%  SETDEVICEID(XID,DEVICEID) sets the device ID of the device.
%  OBJ is the device class instance.  DEVICEID is a string vector,
%  with maximally 32 characters.
%
%  Example:
%    xid = okxem3001v2();
%    xid = setdeviceid(xid, 'Lab 3B XEM3001');
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

if length(stringID) > 32, stringID = stringID(1:32); end;

calllib('okFrontPanel', 'okFrontPanel_SetDeviceID', obj.ptr, stringID);
