function s=isfrontpanelenabled(obj)

%ISFRONTPANELENABLED  Check if FrontPanel is enabled on XEM device.
%  S=ISFRONTPANELENABLED(OBJ) checks whether FrontPanel is enabled
%  on the device.
%  S=1 if FrontPanel is enabled, S=0 if FrontPanel access is not available.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

s = calllib('okFrontPanel', 'okFrontPanel_IsFrontPanelEnabled', obj.ptr);
