function p = getvcop(obj)

%GETVCOP  Returns the current VCO P divider.
%  P = GETVCOP(OBJ) returns the currently programmed
%  P divider in the VCO.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

p = calllib('okFrontPanel', 'okPLL22150_GetVCOP', obj.ptr);
