function freq = getoutputfrequency(obj, n)

%GETOUTPUTFREQUENCY  Returns an output frequency.
%  FREQ=GETOUTPUTFREQUENCY(OBJ,N) returns the output frequency of the
%  N-th PLL output.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

freq = calllib('okFrontPanel', 'okPLL22150_GetOutputFrequency', obj.ptr, n);
