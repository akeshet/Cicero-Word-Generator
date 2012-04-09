function v = getdeviceminorversion(obj)

%GETDEVICEMINORVERSION  Returns the device minor version number.
%  V=GETDEVICEMINORVERSION(OBJ) returns the minor version of the device.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

v = calllib('okFrontPanel', 'okFrontPanel_GetDeviceMinorVersion', obj.ptr);
