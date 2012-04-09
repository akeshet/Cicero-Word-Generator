function display(obj)

%DISPLAY  Display an okPLL22393 object.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 236 $ $Date: 2005-12-10 14:32:28 -0800 (Sat, 10 Dec 2005) $

for j=1:3
	stg = sprintf('PLL%d:  P=%d  Q=%d  Frequency=%.7g MHz', ...
		j, getpllp(obj, j), getpllq(obj, j), getpllfrequency(obj, j));
	disp(stg)
end

for j=1:5
	if (isoutputenabled(obj,j-1)==1)
		en='yes';
	else
		en='no';
	end
	stg = sprintf('Output %d:  Src=%s  Divider=%d  Frequency=%.7g MHz  Enabled:%s', ...
		j, getoutputsource(obj, j-1), getoutputdivider(obj, j-1), getoutputfrequency(obj, j-1), ...
		en );
	disp(stg)
end
