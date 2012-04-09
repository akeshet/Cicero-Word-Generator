function success=isopen(obj)

%ISOPEN  Check if the device is open.
%  S=ISOPEN(OBJ) checks whether the device is open or not.
%  S=1 if device is open, S=0 if device is not open.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

success = calllib('okFrontPanel', 'okFrontPanel_IsOpen', obj.ptr);
