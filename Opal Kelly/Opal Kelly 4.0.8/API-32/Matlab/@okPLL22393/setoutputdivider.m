function obj = setoutputdivider(obj, n, div)

%SETOUTPUTDIVIDER  Sets an outputs divider value.
%  PLL=SETOUTPUTDIVIDER(OBJ, N, DIV) Sets output number N divider to DIV.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 600 $ $Date: 2008-09-02 15:18:59 -0700 (Tue, 02 Sep 2008) $

calllib('okFrontPanel', 'okPLL22393_SetOutputDivider', obj.ptr, n, div);
