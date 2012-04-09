function obj = okpll22393()

%okpll22393  Constructs an okpll22393 object.
%  OBJ=OKPLL22393() constructs the okpll22393 object.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 428 $ $Date: 2006-12-22 16:48:22 -0800 (Fri, 22 Dec 2006) $

if ~libisloaded('okFrontPanel')
	loadlibrary('okFrontPanel', 'okFrontPanelDLL.h');
end

obj.ptr = calllib('okFrontPanel', 'okPLL22393_Construct');
calllib('okFrontPanel', 'okPLL22393_SetCrystalLoad', obj.ptr, 12.0);
calllib('okFrontPanel', 'okPLL22393_SetReference', obj.ptr, 48.0);

obj = class(obj, 'okpll22393');
