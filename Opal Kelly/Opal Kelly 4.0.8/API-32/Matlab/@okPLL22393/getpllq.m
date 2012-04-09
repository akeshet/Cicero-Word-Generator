function q = getpllq(obj, n)

%GETPLLQ  Returns the Q divider for the specified PLL.
%  Q = GETPLLQ(OBJ, N) returns the currently programmed
%  Q divider for PLL number N.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

q = calllib('okFrontPanel', 'okPLL22393_GetPLLQ', obj.ptr, n);
