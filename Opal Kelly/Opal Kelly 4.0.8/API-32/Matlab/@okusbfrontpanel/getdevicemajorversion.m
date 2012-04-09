function v = getdevicemajorversion(obj)

%GETDEVICEMAJORVERSION  Returns the device major version number.
%  V=GETDEVICEMAJORVERSION(OBJ) returns the major version of the device.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

v = calllib('okFrontPanel', 'okFrontPanel_GetDeviceMajorVersion', obj.ptr);
