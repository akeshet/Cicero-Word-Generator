function id = getdeviceid(obj)

%GETDEVICEID  Returns the device ID string.
%  ID=GETDEVICEID(OBJ) returns the device ID string of the device.
%  This string is configurable by the user using SETDEVICEID.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

id = calllib('okFrontPanel', 'okFrontPanel_GetDeviceID', obj.ptr, '                                 ');
