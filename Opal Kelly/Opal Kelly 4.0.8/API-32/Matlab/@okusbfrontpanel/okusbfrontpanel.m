function obj = okusbfrontpanel(ptr)

%okusbfrontpanel  Constructs an okusbfrontpanel object.
%  OBJ=OKUSBFRONTPANEL(PTR) constructs an empty okusbfrontpanel Matlab
%  object.  This is required for the inheritance model of Matlab, but 
%  since we're merely wrapping DLL functions, it doesn't do anything.
%  PTR must be a valid pointer to an okUsbFrontPanel C++ object.
%
%  NOTE: This is called from another method and should not be called
%    by the user.
%
%  Copyright (c) 2005 Opal Kelly Incorporated
%  $Rev: 210 $ $Date: 2005-10-13 19:54:17 -0700 (Thu, 13 Oct 2005) $

obj.ptr=ptr;
obj = class(obj, 'okusbfrontpanel');
