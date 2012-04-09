function success=configurefpga(obj, bitfile)

%CONFIGUREFPGA  Configure the FPGA on the device.
%  S=CONFIGUREFPGA(OBJ,BITFILE) opens the FPGA bitstream and
%  downloads it to the FPGA on the device. BITFILE is a string,
%  which can also include a relative or absolute path.
%
%  S=1 means the configuration was successful, S=0 means the
%  FPGA could not be configured.
%
%  Example:
%    xid = okusbxem3001v2('JsqUWoZUfO');
%    configure(xid,'../fpga/bitfile.bit')
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

success = calllib('okFrontPanel', 'okFrontPanel_ConfigureFPGA', obj.ptr, bitfile);
