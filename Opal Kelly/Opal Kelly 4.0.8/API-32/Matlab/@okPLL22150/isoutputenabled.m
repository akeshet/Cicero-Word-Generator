function y = isoutputenabled(obj, n)

%ISOUTPUTENABLED  Checks if an output is enabled.
%  Y=ISOUTPUTENABLED(OBJ,N) returns 1 if output N is enabled.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

y = calllib('okFrontPanel', 'okPLL22150_IsOutputEnabled', obj.ptr, n);
