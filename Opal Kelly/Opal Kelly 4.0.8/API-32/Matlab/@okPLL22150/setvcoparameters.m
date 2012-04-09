function obj = setvcoparameters(obj, p, q)

%SETVCOPARAMETERS  Sets the P and Q VCO dividers.
%  OBJ=SETVCOPARAMETERS(OBJ,P,Q) sets the P and Q dividers
%  which specify the VCO output frequency.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okPLL22150_SetVCOParameters', obj.ptr, p, q);
