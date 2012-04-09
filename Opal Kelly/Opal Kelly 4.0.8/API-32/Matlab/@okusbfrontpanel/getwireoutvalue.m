function epval = getwireoutvalue(obj, epaddr)

%GETWIREOUTVALUE  Read the WireOut values from the device.
%  epVAL=GETWIREOUTVALUE(OBJ,epADDR) returns the values of the WireOut
%  endpoint in epVAL. The elements of epVAL are unsigned bytes 
%  (8 bits : 0..255) stored as fints (floating point integers).
%  epVAL will have the same dimension as epADDR.  epADDR is a vector or
%  matrix containing the endpoint addresses.
%
%  The valid endpoint address ranges are:
%    0x00-0x1F : WireIn
%  * 0x20-0x3F : WireOut
%    0x40-0x5F : TriggerIn
%    0x60-0x7F : TriggerOut
%    0x80-0x9F : PipeIn
%    0xA0-0xBF : PipeOut
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

epval = zeros(size(epaddr));
for i=1:size(epaddr, 1)
	for j=1:size(epaddr, 2)
		epval(i,j) = calllib('okFrontPanel', 'okFrontPanel_GetWireOutValue', obj.ptr, epaddr(i,j));
	end
end
