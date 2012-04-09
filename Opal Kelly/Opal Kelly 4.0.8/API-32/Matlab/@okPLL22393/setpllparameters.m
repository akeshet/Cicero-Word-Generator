function obj = setpllparameters(obj, n, p, q)

%SETPLLPARAMETERS  Sets the P and Q values for the specified PLL.
%  OBJ=SETPLLPARAMETERS(OBJ,N,P,Q) sets the P and Q values for PLL
%  number N.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 600 $ $Date: 2008-09-02 15:18:59 -0700 (Tue, 02 Sep 2008) $

calllib('okFrontPanel', 'okPLL22393_SetPLLParameters', obj.ptr, n, p, q, 1);
