function obj = setoutputenable(obj, n, en)

%SETOUTPUTENABLE  Enables or disables a PLL output.
%  PLL=SETOUTPUTENABLE(OBJ,N,EN) disables output N if EN=0.  Enables
%  output N otherwise.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okPLL22150_SetOutputEnable', obj.ptr, n, en);
