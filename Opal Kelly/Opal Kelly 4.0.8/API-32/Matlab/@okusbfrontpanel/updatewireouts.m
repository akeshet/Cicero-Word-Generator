function updatewireouts(obj)

%UPDATEWIREOUTS  Update all WireOut endpoints on XEM device.
%  UPDATEWIREOUTS(OBJ) updates all WireOut endpoints on the
%  device.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

calllib('okFrontPanel', 'okFrontPanel_UpdateWireOuts', obj.ptr);
