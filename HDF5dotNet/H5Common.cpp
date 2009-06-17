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
using namespace System::Text;
using namespace System::Runtime::InteropServices;

#include "stdafx.h"
#include <cassert>
#include "H5Common.h"
#include "H5Fpublic.h" // modified version of H5Fpublic.h
#include "H5Ppublic.h" // original HDF5 library header file
#include "H5Gpublic.h" // modified version of H5Gpublic.h

namespace HDF5DotNet
{
   H5Id::H5Id(hid_t id) : id_(id)
   {
   }

   // private default constructor should never be used.
   H5Id::H5Id() : id_(0)
   {
      assert(0);
   }

   // Store the internal HDS5 id.
   H5LocId::H5LocId(hid_t id) : H5Id(id)
   {
   }

   // private default constructor should never be used.
   H5LocId::H5LocId() : H5Id(0)
   {
      assert(0);
   }

   // Store the internal HDF5 id.
   H5FileId::H5FileId(hid_t id) : H5LocId(id)
   {
   }

   // private default constructor should never be used.
   H5FileId::H5FileId() : H5LocId(0)
   {
      assert(0);
   }

   // Store the internal HDF5 id.
   H5GroupId::H5GroupId(hid_t id) : H5LocId(id)
   {
   }

   // private default constructor should never be used.
   H5GroupId::H5GroupId() : H5LocId(0)
   {
      assert(0);
   }

   // Store the internal HDS5 id.
   H5DataSpaceId::H5DataSpaceId(hid_t id) : H5Id(id)
   {
   }

   H5DataSpaceId::H5DataSpaceId(H5S::H5SType h5Stype) : 
      H5Id(safe_cast<hid_t>(h5Stype))
   {
   }

   // private default constructor should never be used.
   H5DataSpaceId::H5DataSpaceId() : H5Id(0)
   {
      assert(0);
   }
   
   H5DataSetId::H5DataSetId(H5S::H5SType h5DatasetType) :
      H5Id(safe_cast<hid_t>(h5DatasetType))
   {
   }

   // Store the internal HDS5 id.
   H5DataSetId::H5DataSetId(hid_t id) : H5Id(id)
   {
   }

   // private default constructor should never be used.
   H5DataSetId::H5DataSetId() : H5Id(0)
   {
      assert(0);
   }

   // Store the internal HDS5 id.
   H5DataTypeId::H5DataTypeId(hid_t id) : H5Id(id)
   {
   }

   // Allow the implicit construction of a type Id from a H5T::H5Type.
   H5DataTypeId::H5DataTypeId(H5T::H5Type h5type) :
      H5Id(safe_cast<hid_t>(H5T::getStdType(h5type)))
   {
   }

   // private default constructor should never be used.
   H5DataTypeId::H5DataTypeId() : H5Id(0)
   {
      assert(0);
   }

   // Store the internal H5PropertyList id.
   H5PropertyListId::H5PropertyListId(hid_t id) : H5Id(id)
   {
   }

   H5PropertyListId::H5PropertyListId(H5P::Template h5PTemplate) : 
      H5Id(safe_cast<hid_t>(h5PTemplate))
   {
   }

   // private default constructor should never be used.
   H5PropertyListId::H5PropertyListId() : H5Id(0)
   {
      assert(0);
   }
}

