function display(obj)

%DISPLAY  Display an okPLL22150 object.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 209 $ $Date: 2005-10-13 19:40:13 -0700 (Thu, 13 Oct 2005) $

stg = sprintf('VCO:  P=%d  Q=%d  Frequency=%.7g MHz', ...
	getvcop(obj), getvcoq(obj), getvcofrequency(obj));
disp(stg)

stg = sprintf('Divider 1:  Src=%s  N=%d', ...
	getdiv1source(obj), getdiv1divider(obj));
disp(stg)
stg = sprintf('Divider 2:  Src=%s  N=%d', ...
	getdiv2source(obj), getdiv2divider(obj));
disp(stg)

for j=1:6
	if (isoutputenabled(obj,j-1)==1)
		en='yes';
	else
		en='no';
	end
	stg = sprintf('Output %d:  Src=%s  Frequency=%.7g MHz  Enabled:%s', ...
		j, getoutputsource(obj, j-1), getoutputfrequency(obj, j-1), ...
		en );
	disp(stg)
end
