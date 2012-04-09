function y = getdiv2source(obj)

%GETDIV2SOURCE  Get the divider 2 source.
%  Y=GETDIV2SOURCE(OBJ) returns the divider 2 source in Y.
%  The result is a string representing the source:
%     'DivSrc_Ref'
%     'DivSrc_VCO'
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

y = calllib('okFrontPanel', 'okPLL22150_GetDiv2Source', obj.ptr);
