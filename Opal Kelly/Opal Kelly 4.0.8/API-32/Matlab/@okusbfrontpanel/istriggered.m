function trig = istriggered(obj, epaddr, epmask)

%ISTRIGGERED  Checks if a TriggerOut is triggered.
%  TRIG=ISTRIGGERED(OBJ,epADDR,epMASK) checks if a TriggerOut is
%  triggered. If TRIG = 1, the trigger has been activated, if
%  TRIG = 0, the trigger has not be activated.
%  epADDR is a scalar containing the TriggerOut endpoint address
%  and epMASK contains the mask with which the trigger is checked.
%
%  The valid endpoint address ranges are:
%
%    0x00-0x1F : WireIn
%    0x20-0x3F : WireOut
%    0x40-0x5F : TriggerIn
%  * 0x60-0x7F : TriggerOut
%    0x80-0x9F : PipeIn
%    0xA0-0xBF : PipeOut
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 971 $ $Date: 2011-05-27 06:59:56 -0700 (Fri, 27 May 2011) $

trig = calllib('okFrontPanel', 'okFrontPanel_IsTriggered', obj.ptr, epaddr, epmask);
