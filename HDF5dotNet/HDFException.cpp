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
using namespace System;
//using namespace System::ApplicationException;

#include "stdafx.h"
#include "HDFException.h"

namespace HDF5DotNet
{
/// <summary>
// HDFException is the base class for HDFExceptions.
/// </summary>
HDFException::HDFException()
{
}

   HDFException::HDFException(String^ message, unsigned int errorCode) : 
      ApplicationException(message), errorCode_(errorCode)
{
}

      
   HDFException::HDFException(String^ message, unsigned int errorCode,
			      Exception^ innerEx) : 
      ApplicationException(message, innerEx), errorCode_(errorCode)
{
}

}


