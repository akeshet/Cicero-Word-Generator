function p = getpllp(obj, n)

%GETPLLP  Returns the P multiplier for the specified PLL.
%  P = GETPLLP(OBJ, N) returns the currently programmed
%  P multiplier for PLL number N.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

p = calllib('okFrontPanel', 'okPLL22393_GetPLLP', obj.ptr, n);
