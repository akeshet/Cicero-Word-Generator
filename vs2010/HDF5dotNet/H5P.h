/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright by The HDF Group (THG).                                       *
 * All rights reserved.                                                    *
 *                                                                         *
 * This file is part of HDF5.  The full HDF5 copyright notice, including   *
 * terms governing use, modification, and redistribution, is contained in  *
 * the files COPYING and Copyright.html.  COPYING can be found at the root *
 * of the source code distribution tree; Copyright.html can be found at the*
 * root level of an installed copy of the electronic HDF5 document set and *
 * is linked from the top-level documents page.  It can also be found at   *
 * http://www.hdfgroup.org/HDF5/doc/Copyright.html.  If you do not have    *
 * access to either file, you may request a copy from help@hdfgroup.org.   *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
#pragma once
using namespace System;
#include "stdafx.h"

#include "H5Ppublic.h"
#include "HDFException.h"
#include "HDFExceptionSubclasses.h"

namespace HDF5DotNet
{
   public ref class H5P
   {
      public:
	 enum class Template
	 {
	       DEFAULT = H5P_DEFAULT
	 };
   };
      
   /// <summary>
   /// The H5P contains static member function for each of the supported
   /// H5P calls of the HDF5 library.
   /// </summary>
//public ref class H5P
 //  {
 //  };
}
