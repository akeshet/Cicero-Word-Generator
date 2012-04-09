function y = isoutputenabled(obj, n)

%ISOUTPUTENABLED  Checks if an output is enabled.
%  Y=ISOUTPUTENABLED(OBJ,N) returns 1 if output N is enabled.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

y = calllib('okFrontPanel', 'okPLL22393_IsOutputEnabled', obj.ptr, n);
