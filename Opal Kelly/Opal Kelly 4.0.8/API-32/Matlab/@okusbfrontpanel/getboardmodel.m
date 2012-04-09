function epval = getboardmodel(obj)

%GETBOARDMODEL  Returns the board model of the current device.
%  GETBOARDMODEL(OBJ) returns the board model of the current
%  device, as a string:
%     'brdUnknown'
%     'brdXEM3001v1'
%     'brdXEM3001v2'
%     'brdXEM3001CL'
%     'brdXEM3005'
%     'brdXEM3010'
%     'brdXEM3020'
%     'brdXEM3050'
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

brd = calllib('okFrontPanel', 'okFrontPanel_GetBoardModel', obj.ptr);
if brd==0,
  brdModel = 'brdUnknown'
elsif brd==1,
  brdModel = 'brdXEM3001v1'
elsif brd==2,
  brdModel = 'brdXEM3001v2'
elsif brd==3,
  brdModel = 'brdXEM3010'
elsif brd==4,
  brdModel = 'brdXEM3005'
elsif brd==5,
  brdModel = 'brdXEM3001CL'
elsif brd==6,
  brdModel = 'brdXEM3020'
elsif brd==7,
  brdModel = 'brdXEM3050'
elsif brd==8,
  brdModel = 'brdXEM9002'
elsif brd==9,
  brdModel = 'brdXEM3001RB'
elsif brd==10,
  brdModel = 'brdXEM5010'
elsif brd==11,
  brdModel = 'brdXEM6110LX45'
elsif brd==15,
  brdModel = 'brdXEM6110LX150'
elsif brd==12,
  brdModel = 'brdXEM6001'
elsif brd==13,
  brdModel = 'brdXEM6010LX45'
elsif brd==14,
  brdModel = 'brdXEM6010LX150'
end

brdModel
