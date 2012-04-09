function freq = getoutputfrequency(obj, n)

%GETOUTPUTFREQUENCY  Returns an output frequency.
%  FREQ=GETOUTPUTFREQUENCY(OBJ,N) returns the output frequency of the
%  output number N.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

freq = calllib('okFrontPanel', 'okPLL22393_GetOutputFrequency', obj.ptr, n);
