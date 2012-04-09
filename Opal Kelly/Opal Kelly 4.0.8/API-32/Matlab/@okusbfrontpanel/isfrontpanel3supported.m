function s=isfrontpanel3supported(obj)

%ISFRONTPANEL3SUPPORTED  Check if FrontPanel-3 is enabled on XEM device.
%  S=ISFRONTPANEL3SUPPORTED(OBJ) checks whether FrontPanel-3 support
%  is enabled on the device (by FrontPanel-3 firmware).
%  S=1 if FrontPanel-3 is enabled, S=0 otherwise
%
%  Copyright (c) 2006 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

s = calllib('okFrontPanel', 'okFrontPanel_IsFrontPanel3Supported', obj.ptr);
