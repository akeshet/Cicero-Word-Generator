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

#include "H5F.h"

// This prototype is necessary to translate .NET string prototype
// to null-terminated ANSI string, char*, in the HDF library.
// Note: This differs from the current prototype in H5Fpublic.h and
// it will go in H5FpublicNET.h
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Fclose(hid_t id);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Fcreate([MarshalAs(UnmanagedType::LPStr)] 
				String^ filename, 
				unsigned int flags, 
				hid_t creationPropertyList, 
				hid_t accessPropertyList);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Fopen( [MarshalAs(UnmanagedType::LPStr)] 
			       String^ groupName, 
			       unsigned int flags, 
			       hid_t accessId);

namespace HDF5DotNet
{
   H5FileId^ H5F::create(String^ filename,
			 CreateMode mode,
			 H5PropertyListId^ create_id,
			 H5PropertyListId^ access_id)
   {
      // Make the call to the "C" library.
      int id = H5Fcreate(filename, safe_cast<int>(mode), 
		create_id->Id, access_id->Id);
      
      // Check conditions and throw appropriately.
      if(id < 0)
      {
	 throw gcnew 
	    H5FcreateException("File \"" + filename + " failed to create.",
	    id);
      }
      
      // Put the returned id into a H5Id object.
      H5FileId^ returnH5Id = gcnew H5FileId(id);

      return returnH5Id;
   }


   H5FileId^ H5F::create(String^ filename,
		    CreateMode mode,
		    H5PropertyListId^ create_id)
   {
      return create(filename, mode, create_id, gcnew H5PropertyListId(H5P_DEFAULT));
   }
   
   H5FileId^ H5F::create(String^ filename,
		    CreateMode mode)
   {
      return create(filename, mode, gcnew H5PropertyListId(H5P_DEFAULT), 
		    gcnew H5PropertyListId(H5P_DEFAULT));
   }

   H5FileId^ H5F::open(String^ filename, OpenMode mode)
   {
      // Make the call to the "C" library.
      int id = H5Fopen(filename, safe_cast<unsigned int>(mode), H5P_DEFAULT);
      
      // Check conditions and throw appropriately.
      if(id < 0)
      {
	    throw gcnew 
	    H5FopenException("File \"" + filename + 
			     " failed to open with status " + id,
			     id);
      }
      
      // Put the returned id into a H5Id object.
      H5FileId^ returnH5Id = gcnew H5FileId(id);

      return returnH5Id;
   }
   
   
   /// <summary> open an existing HDF5 file. </summary>
   H5FileId^ H5F::open(String^ filename,
		       OpenMode mode, 
		       H5PropertyListId^ propertyListId)
   {
      // Make the call to the "C" library.
      int id = H5Fopen(filename, safe_cast<unsigned int>(mode), 
		       propertyListId->Id);
      
      // Check conditions and throw appropriately.
      if(id < 0)
      {
	 throw gcnew 
	    H5FopenException("File \"" + filename + 
			     " failed to open with status " + id,
			     id);
      }
      
      // Put the returned id into a H5Id object.
      H5FileId^ returnH5Id = gcnew H5FileId(id);

      return returnH5Id;
   }
   
   
   void H5F::close(H5FileId^ id)
   {
      hid_t status = H5Fclose(id->Id);

      if (status < 0)
      {
	 String^ message = 
	    String::Format(
	       "H5F.close: file id: {0:x} failed with error code {1:x}",
	       id->Id, status);
	 
	 throw gcnew H5FcloseException(message, status);
      }
   }
}// end of namespace HDF5dotNET
