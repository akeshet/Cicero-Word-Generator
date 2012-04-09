function epval = getlasttransferlength(obj)

%GETLASTTRANSFERLENGTH  Read the length of the last transfer.
%  GETLASTTRANSFERLENGTH(OBJ) returns the length of the last transfer
%  (in bytes).  This returns the length as determined by the USB 
%  interface on the PC.  In the case of a failed transfer, this may not
%  be the same as the length of data read from the FPGA since some
%  bytes may be lost to buffering.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

calllib('okFrontPanel', 'okFrontPanel_GetLastTransferLength', obj.ptr);
