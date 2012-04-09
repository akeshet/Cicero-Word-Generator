function y = getoutputdivider(obj, n)

%GETOUTPUTDIVIDER  Get the output divider value.
%  Y=GETOUTPUTDIVIDER(OBJ, N) returns the output divider value for
%  output number N.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

y = calllib('okFrontPanel', 'okPLL22393_GetOutputDivider', obj.ptr, n);
