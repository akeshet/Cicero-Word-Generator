function success = writetopipein(obj, epaddr, epvalue, psize)

%WRITETOPIPEIN  Write data into a PipeIn.
%  SUCCESS=WRITETOPIPEIN(OBJ,epADDR,epVALUE) writes
%  the elements of the vector epVALUE into a PipeIn endpoint.
%  The elements of epVALUE need to be unsigned bytes (8 bits : 0..255)
%  stored as fints (floating point integers).
%  epADDR the endpoint address of the PipeIn endpoint.
%
%  SUCCESS=WRITETOPIPEIN(OBJ,epADDR,epVALUE,PSIZE) subdivides
%  transfer into multiple packets. The PSIZE contains the
%  packet size of each packet, except for the last one.
%
%  The valid endpoint address ranges are:
%
%    0x00-0x1F : WireIn
%    0x20-0x3F : WireOut
%    0x40-0x5F : TriggerIn
%    0x60-0x7F : TriggerOut
%  * 0x80-0x9F : PipeIn
%    0xA0-0xBF : PipeOut
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

bsize = prod(size(epvalue));
if ~exist('psize' ,'var')   | isempty(psize); psize = bsize; end;
psize = min(psize,bsize);

if (bsize == psize),
	success = calllib('okFrontPanel', 'okFrontPanel_WriteToPipeIn', obj.ptr, epaddr, psize, epvalue);
else
	kk = (1:psize)';
	for k = 1:fix(bsize/psize),
		success = calllib('okFrontPanel', 'okFrontPanel_WriteToPipeIn', obj.ptr, epaddr, psize, epvalue(kk));
		kk = kk+psize;
	end
	psize_last = mod(bsize,psize);
	kk = kk(1:psize_last);
	success = calllib('okFrontPanel', 'okFrontPanel_WriteToPipeIn', obj.ptr, epaddr, psize_last, epvalue(kk));
end
