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
// This is the main DLL file.
#include "stdafx.h"

using namespace System;
using namespace System::Text;
using namespace System::Runtime::InteropServices;

#include "H5T.h"
#include "H5S.h"
#include "H5P.h"
#include "H5E.h"
#include "H5Epublic.h"
//#include "H5Common.h"

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Eset_auto(H5E_auto_t func, void* clientData);

namespace HDF5DotNet
{
   void H5E::suppressPrinting()
   {
      // Use H5Eset_auto to suppress printing
      herr_t status = H5Eset_auto(0,0);
      
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5E.suppressPrinting: \n"
	    "  Failed to suppress printing with status = {0}\n",
	    status);

	 throw gcnew H5EsuppressPrintingException(message, status);
      }
   }
}


