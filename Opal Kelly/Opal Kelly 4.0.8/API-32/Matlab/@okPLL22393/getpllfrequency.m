function freq = getpllfrequency(obj, n)

%GETPLLFREQUENCY  Returns a PLL's VCO frequency.
%  FREQ=GETPLLFREQUENCY(OBJ, N) returns the current VCO frequency
%  for PLL number N.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

freq = calllib('okFrontPanel', 'okPLL22393_GetPLLFrequency', obj.ptr, n);
