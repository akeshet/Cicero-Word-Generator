function q = getvcoq(obj)

%GETVCOQ  Returns the current VCO Q divider.
%  Q = GETVCOQ(OBJ) returns the currently programmed
%  Q divider in the VCO.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

q = calllib('okFrontPanel', 'okPLL22150_GetVCOQ', obj.ptr);
