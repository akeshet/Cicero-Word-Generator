function obj = setoutputenable(obj, n, en)

%SETOUTPUTENABLE  Enables or disables a PLL output.
%  PLL=SETOUTPUTENABLE(OBJ,N,EN) disables output N if EN=0.  Enables
%  output N otherwise.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

calllib('okFrontPanel', 'okPLL22393_SetOutputEnable', obj.ptr, n, en);
