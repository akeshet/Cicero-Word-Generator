function [mlist,snlist]=ok_getdevicelist()

%OK_GETDEVICELIST  Returns a list of attached XEM devices.
%  [M,SN]=OK_GETDEVICELIST() looks for attached XEM devices.
%  It returns the device models in M, serial numbers in SN.
%  The serial number can then be used to open an attached XEM
%  for use.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 972 $ $Date: 2011-05-27 09:03:26 -0700 (Fri, 27 May 2011) $

if ~libisloaded('okFrontPanel')
	loadlibrary('okFrontPanel', 'okFrontPanelDLL.h');
end

% Try to construct an okUsbFrontPanel and open it (the board model doesn't matter).
xptr = calllib('okFrontPanel', 'okFrontPanel_Construct');
num = calllib('okFrontPanel', 'okFrontPanel_GetDeviceCount', xptr);
for j=1:num
   m = calllib('okFrontPanel', 'okFrontPanel_GetDeviceListModel', xptr, j-1);
   sn = calllib('okFrontPanel', 'okFrontPanel_GetDeviceListSerial', xptr, j-1, '           ', 10);
   if ~exist('snlist', 'var')
      mlist = m;
      snlist = sn;
   else
      mlist = [mlist;m];
      snlist = char(snlist, sn);
   end
end
calllib('okFrontPanel', 'okFrontPanel_Destruct', xptr);
