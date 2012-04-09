function y = getoutputsource(obj, n)

%GETOUTPUTSOURCE  Get an output source.
%  Y=GETOUTPUTSOURCE(OBJ, N) returns the source for output N.
%  The result is a string representing the source:
%     'ClkSrc_Ref'
%     'ClkSrc_Div1ByN'
%     'ClkSrc_Div1By2'
%     'ClkSrc_Div1By3'
%     'ClkSrc_Div2ByN'
%     'ClkSrc_Div2By2'
%     'ClkSrc_Div2By4'
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

y = calllib('okFrontPanel', 'okPLL22150_GetOutputSource', obj.ptr, n);
