function freq = getvcofrequency(obj)

%GETVCOFREQUENCY  Returns the VCO frequency.
%  FREQ=GETVCOFREQUENCY(OBJ) returns the currently programmed VCO frequency.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

freq = calllib('okFrontPanel', 'okPLL22150_GetVCOFrequency', obj.ptr);
