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

#include "H5Array.h"
#include "H5D.h"
#include "H5T.h"
#include "H5S.h"
#include "H5P.h"
//#include "H5Common.h"

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Dcreate(hid_t fileId,
				  [MarshalAs(UnmanagedType::LPStr)] 
				  String^ dataSetName,
				  hid_t dataType,
				  hid_t dataSpaceId,
				  hid_t create);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Dclose(hid_t id);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Dwrite(hid_t dataSetId, 
				  hid_t memType,
				  hid_t memSpace, 
				  hid_t fileSpace, 
				  hid_t xfer, 
				  void* data);
				  //[In, MarshalAs(UnmanagedType::LPArray)]  
				  //array<int>^ data);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Dread(hid_t dataSetId, 
				 hid_t memType,
				 hid_t memSpace, 
				 hid_t fileSpace, 
				 hid_t xfer, 
				 void* data);


[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Dopen(hid_t groupOrFileId,
				 [MarshalAs(UnmanagedType::LPStr)] 
				 String^ dataSetName);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Dget_type(hid_t dataSetId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Dget_space(hid_t dataSetId);

namespace HDF5DotNet
{
   H5DataSetId^ H5D::create(H5LocId^ groupOrFileId, 
			    String^ datasetName,
			    H5T::H5Type dataType,
			    H5DataSpaceId^ dataSpaceId)
   {
      hid_t status = H5Dcreate(groupOrFileId->Id, 
			       datasetName, 
			       H5T::getStdType(dataType),
			       dataSpaceId->Id,
			       0);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.create: \n"
	    "  Failed to create dataset with status = {0}\n"
	    "    groupOrFileId = {1}\n"
	    "    datasetName = {2}\n"
	    "    dataType = {3}\n"
		"    dataSpaceId = {4}\n",
	    status, groupOrFileId->Id, datasetName, dataType, dataSpaceId->Id);
	    throw gcnew H5DcreateException(message, status);
      }
      
      return gcnew H5DataSetId(status);
   }

   H5DataSetId^ H5D::create(H5LocId^ groupOrFileId, 
		     String^ datasetName,
		     H5DataTypeId^ dataType,
		     H5DataSpaceId^ dataSpaceId)
   {
      hid_t status = H5Dcreate(groupOrFileId->Id, 
			       datasetName, 
			       dataType->Id,
				   dataSpaceId->Id,
			       0);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.create: \n"
	    "  Failed to create dataset with status = {0}\n"
	    "    groupOrFileId = {1}\n"
        "    datasetName = {2}\n"
		"    dataType = {3}\n"
        "    dataspaceId = {4}\n",
	    status, groupOrFileId->Id, datasetName, dataType->Id, 
	    dataSpaceId->Id);
	    throw gcnew H5DcreateException(message, status);
      }
      
      return gcnew H5DataSetId(status);
   }

   void H5D::close(H5DataSetId^ id)	
   {
      herr_t status = H5Dclose(id->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.close: \n"
	    "  Failed to close data set {0:x} with status {1}\n",
	    id->Id, status);

	 throw gcnew H5DcloseException(message, status);
      }
   }

   generic <class Type>
   void H5D::write(H5DataSetId^ dataSetId,
		   H5DataTypeId^ memTypeId,
		   H5DataSpaceId^ memSpaceId,
		   H5DataSpaceId^ fileSpaceId,
		   H5PropertyListId^ xferPropListId,
		   H5Array<Type>^ data)
   {
      pin_ptr<Type> pinnedDataPtr = data->getDataAddress();
      void* voidPtr = pinnedDataPtr;

      // Write using the pinned array
      herr_t status = H5Dwrite(dataSetId->Id, 
			       memTypeId->Id,
			       memSpaceId->Id, fileSpaceId->Id, 
			       xferPropListId->Id, voidPtr);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.write: \n"
	    "Failed to write data to data set {0:x} with status {1}\n",
	    dataSetId->Id, status);
	 
	 throw gcnew H5DwriteException(message, status);
      }
   }
   
   generic <class Type>
   void H5D::writeScalar(H5DataSetId^ dataSetId,
		   H5DataTypeId^ memTypeId,
		   H5DataSpaceId^ memSpaceId,
		   H5DataSpaceId^ fileSpaceId,
		   H5PropertyListId^ xferPropListId,
		   Type% data)
   {
      pin_ptr<Type> pinnedDataPtr = &data;
      void* voidPtr = pinnedDataPtr;

      // Write using the pinned array
      herr_t status = H5Dwrite(dataSetId->Id, 
			       memTypeId->Id,
			       memSpaceId->Id, fileSpaceId->Id, 
			       xferPropListId->Id, voidPtr);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.write: \n"
	    "Failed to write data to data set {0:x} with status {1}\n",
	    dataSetId->Id, status);
	 
	 throw gcnew H5DwriteException(message, status);
      }
   }

   generic <class Type>
   void H5D::write(H5DataSetId^ dataSetId,
			  H5DataTypeId^ dataType, 
			  H5Array<Type>^ data)
   {
      // Write using the pinned array
	  H5D::write(dataSetId, 
		     dataType,
		     gcnew H5DataSpaceId(H5S::H5SType::ALL), 
		     gcnew H5DataSpaceId(H5S::H5SType::ALL),
		     gcnew H5PropertyListId(H5P::Template::DEFAULT), 
		     data);
      
   }
   
   generic <class Type>
   void H5D::writeScalar(H5DataSetId^ dataSetId,
			  H5DataTypeId^ dataType, 
			  Type% data)
   {
	  H5D::writeScalar(dataSetId, 
		     dataType,
		     gcnew H5DataSpaceId(H5S::H5SType::ALL), 
		     gcnew H5DataSpaceId(H5S::H5SType::ALL),
		     gcnew H5PropertyListId(H5P::Template::DEFAULT), 
		     data);
   }
   
   generic <class Type>
   void H5D::read(H5DataSetId^ dataSetId,
		   H5DataTypeId^ memTypeId,
		   H5DataSpaceId^ memSpaceId,
		   H5DataSpaceId^ fileSpaceId,
		   H5PropertyListId^ xferPropListId,
		   H5Array<Type>^ data)
   {
      pin_ptr<Type> pinnedDataPtr = data->getDataAddress();
      void* voidPtr = pinnedDataPtr;

      // Read using the pinned array
      herr_t status = H5Dread(dataSetId->Id, 
			      memTypeId->Id,
			      memSpaceId->Id, fileSpaceId->Id, 
			      xferPropListId->Id, voidPtr);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.read: \n"
	    "Failed to read data to data set {0:x} with status {1}\n",
	    dataSetId->Id, status);
	 
	 throw gcnew H5DreadException(message, status);
      }
   }

   generic <class Type>
   void H5D::readScalar(H5DataSetId^ dataSetId,
			H5DataTypeId^ memTypeId,
			H5DataSpaceId^ memSpaceId,
			H5DataSpaceId^ fileSpaceId,
			H5PropertyListId^ xferPropListId,
			Type% data)
   {
      pin_ptr<Type> pinnedDataPtr = &data;
      void* voidPtr = pinnedDataPtr;

      // Read using the pinned array
      herr_t status = H5Dread(dataSetId->Id, 
			      memTypeId->Id,
			      memSpaceId->Id, fileSpaceId->Id, 
			      xferPropListId->Id, voidPtr);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5D.read: \n"
	    "Failed to read data to data set {0:x} with status {1}\n",
	    dataSetId->Id, status);
	 
	 throw gcnew H5DreadException(message, status);
      }
   }
   
   generic <class Type>
   void H5D::read(H5DataSetId^ dataSetId, H5DataTypeId^ dataType, 
		  H5Array<Type>^ data)
   {
      read(dataSetId, 
	   dataType,
	   gcnew H5DataSpaceId(H5S::H5SType::ALL),
	   gcnew H5DataSpaceId(H5S::H5SType::ALL),
	   gcnew H5PropertyListId(H5P::Template::DEFAULT), 
	   data);
   }

   generic <class Type>
   void H5D::readScalar(H5DataSetId^ dataSetId,
            H5DataTypeId^ dataType,
		    Type% data)
   {
      readScalar(dataSetId, 
	   dataType,
	   gcnew H5DataSpaceId(H5S::H5SType::ALL),
	   gcnew H5DataSpaceId(H5S::H5SType::ALL),
	   gcnew H5PropertyListId(H5P::Template::DEFAULT), 
	   data);
   }
   
   H5DataSetId^ H5D::open(H5LocId^ groupOrFileId, String^ dataSetName)
   {
      herr_t status = H5Dopen(groupOrFileId->Id, dataSetName);

      if (status < 0)
      {
	 String^ message = String::Format(
	 "H5D.open: \n"
	 "Failed to open data set {0} from groupOrFileId {1} with status {2}\n",
	    dataSetName, groupOrFileId->Id, status);
	 
	 throw gcnew H5DopenException(message, status);
	  }

      return gcnew H5DataSetId(status);
   }

   H5DataSpaceId^ H5D::getSpace(H5DataSetId^ dataSetId)
   {
      herr_t status = H5Dget_space(dataSetId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	 "H5D.getSpace: \n"
	 "Failed to get space for dataSetid {0} with status {1}\n",
	    dataSetId->Id, status);

	 throw gcnew H5DgetSpaceException(message, status);
      }

      return gcnew H5DataSpaceId(status);
   }
   
   H5DataTypeId^ H5D::getType(H5DataSetId^ dataSetId)
   {
      herr_t status = H5Dget_type(dataSetId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	 "H5D.getType: \n"
	 "Failed to get type for dataSetid {0} with status {1}\n",
	    dataSetId->Id, status);

	 throw gcnew H5DgetTypeException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }
   
}
