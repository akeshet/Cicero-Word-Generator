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

#include "H5Common.h"

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Screate_simple(
   int rank, 
   [MarshalAs(UnmanagedType::LPArray)]  array<hsize_t>^ dims,
   [MarshalAs(UnmanagedType::LPArray)]  array<hsize_t>^ maxDims);

extern "C" hid_t _cdecl H5Sget_simple_extent_dims(
   hid_t dataSpaceId, 
   [MarshalAs(UnmanagedType::LPArray)]  array<hsize_t>^ dims,
   [MarshalAs(UnmanagedType::LPArray)]  array<hsize_t>^ maxDims);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Sselect_hyperslab(
   hid_t id, 
   H5S_seloper_t selectOperator,
   [MarshalAs(UnmanagedType::LPArray)]  array<hsize_t>^ start,
   const hsize_t* stride,
   [MarshalAs(UnmanagedType::LPArray)]  array<hsize_t>^ count,
   const hsize_t* block);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Sclose(hid_t id);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Screate(H5S_class_t id);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Sget_simple_extent_ndims(hid_t id);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Sselect_none(hid_t id);


namespace HDF5DotNet
{
   H5DataSpaceId^ H5S::create_simple(int rank, 
				     array<hsize_t>^ dims,
				     array<hsize_t>^ maxDims)
   {
      hid_t status = H5Screate_simple(rank, dims, maxDims);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.create_simple: \n"
	    "  Failed to create data space with status = {0}\n"
	    "    rank = {1}\n"
            "    dims = {2}\n"
            "    maxDims = {3}\n",
	    status, rank, dims, maxDims);
	 throw gcnew H5ScreateSimpleException(message, status);
      }
      
      return gcnew H5DataSpaceId(status);
   }
   
   H5DataSpaceId^ H5S::create_simple(int rank, 
				     array<hsize_t>^ dims)
   {
      array<hsize_t>^ copyOfDims = dims;
      hid_t status = H5Screate_simple(rank, dims, copyOfDims);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.create_simple: \n"
	    "  Failed to create data space with status = {0}\n"
	    "    rank = {1}\n"
            "    dims = {2}\n",
	    status, rank, dims);
	 throw gcnew H5ScreateSimpleException(message, status);
      }
      
      return gcnew H5DataSpaceId(status);
   }
   
   void H5S::close(H5DataSpaceId^ id)	
   {
      herr_t status = H5Sclose(id->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.close: \n"
	    "  Failed to close data space {0:x} with status {1}\n",
	    id->Id, status);

	 throw gcnew H5ScloseException(message, status);
      }
   }

   H5DataSpaceId^ H5S::create(H5SClass createClass)
   {
      hid_t status = H5Screate(safe_cast<H5S_class_t>(createClass));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.create: \n"
	    "  Failed to create data space of class {0} with status {1}\n",
	    createClass, status);

	 throw gcnew H5ScreateException(message, status);
      }

      return gcnew H5DataSpaceId(status);
   }

   void H5S::selectStridedHyperslab(H5DataSpaceId^ spaceId,
				    H5S::SelectOperator selectOperator,
				    array<hsize_t>^ start,
				    array<hsize_t>^ stride,
				    array<hsize_t>^ count,
				    array<hsize_t>^ block)
   {
      // Create a pinned pointer so that garbage collection/compaction 
      //will not move the stride array
      pin_ptr<hsize_t> pinnedStridePtr(&stride[0]);

      // Get a hsize_t pointer to the pinned array.
      hsize_t* stridePtr = pinnedStridePtr;

      // Create a pinned pointer so that garbage collection/compaction 
      //will not move the block array
      pin_ptr<hsize_t> pinnedBlockPtr(&block[0]);

      // Get a hsize_t pointer to the pinned array.
      hsize_t* blockPtr = pinnedBlockPtr;

      hid_t status = H5Sselect_hyperslab(
	 spaceId->Id,
	 safe_cast<H5S_seloper_t>(selectOperator),
	 start,
	 stridePtr,
	 count,
	 blockPtr);
      

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.selectStridedHyperslab: \n"
	    "  Failed to select hyperslabe with: \n"
	    "  id = {0} \n"
            "  selectOperator = {1}\n"
	    "  start = {2}\n"
	    "  stride = {3}\n"
	    "  count = {4}\n"
	    "  block = {5}\n"
	    "  status = {6}\n",
	    spaceId->Id, selectOperator, start, stride, count, block, status);

	 throw gcnew H5SselectStridedHyperslabException(message, status);
      }
   }
	 
				     
   void H5S::selectStridedHyperslab(H5DataSpaceId^ spaceId,
				    H5S::SelectOperator selectOperator,
				    array<hsize_t>^ start,
				    array<hsize_t>^ stride,
				    array<hsize_t>^ count)
   {
      // Create a pinned pointer so that garbage collection/compaction 
      //will not move the stride array
      pin_ptr<hsize_t> pinnedStridePtr(&stride[0]);

      // Get a hsize_t pointer to the pinned array.
      hsize_t* stridePtr = pinnedStridePtr;

      hid_t status = H5Sselect_hyperslab(
	 spaceId->Id,
	 safe_cast<H5S_seloper_t>(selectOperator),
	 start,
	 stridePtr,
	 count,
	 0);
      

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.selectStridedHyperslab: \n"
	    "  Failed to select hyperslabe with: \n"
	    "  id = {0} \n"
            "  selectOperator = {1}\n"
	    "  start = {2}\n"
	    "  stride = {3}\n"
	    "  count = {4}\n"
	    "  status = {5}\n",
	    spaceId->Id, selectOperator, start, stride, count, status);

	 throw gcnew H5SselectStridedHyperslabException(message, status);
      }
   }
   
	 
   void H5S::selectHyperslab(H5DataSpaceId^ spaceId,
			     H5S::SelectOperator selectOperator,
			     array<hsize_t>^ start,
			     array<hsize_t>^ count,
			     array<hsize_t>^ block)
   {
      // Create a pinned pointer so that garbage collection/compaction 
      //will not move the block array
      pin_ptr<hsize_t> pinnedBlockPtr(&block[0]);

      // Get a hsize_t pointer to the pinned array.
      hsize_t* blockPtr = pinnedBlockPtr;

      hid_t status = H5Sselect_hyperslab(
	 spaceId->Id,
	 safe_cast<H5S_seloper_t>(selectOperator),
	 start,
	 0,
	 count,
	 blockPtr);
      

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.selectStridedHyperslab: \n"
	    "  Failed to select hyperslabe with: \n"
	    "  id = {0} \n"
            "  selectOperator = {1}\n"
	    "  start = {2}\n"
	    "  count = {3}\n"
	    "  block = {4}\n"
	    "  status = {5}\n",
	    spaceId->Id, selectOperator, start, count, block, status);

	 throw gcnew H5SselectStridedHyperslabException(message, status);
      }
   }
   
	 
				     
   void H5S::selectHyperslab(H5DataSpaceId^ spaceId,
			     H5S::SelectOperator selectOperator,
			     array<hsize_t>^ start,
			     array<hsize_t>^ count)
   {
      hid_t status = H5Sselect_hyperslab(
	 spaceId->Id,
	 safe_cast<H5S_seloper_t>(selectOperator),
	 start,
	 0,
	 count,
	 0);
      

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.selectStridedHyperslab: \n"
	    "  Failed to select hyperslabe with: \n"
	    "  id = {0} \n"
            "  selectOperator = {1}\n"
	    "  start = {2}\n"
	    "  count = {3}\n"
	    "  status = {4}\n",
	    spaceId->Id, selectOperator, start, count, status);

	 throw gcnew H5SselectStridedHyperslabException(message, status);
      }
   }

   int H5S::getSimpleExtentNDims(H5DataSpaceId^ spaceId)
   {
      hid_t status = H5Sget_simple_extent_ndims(spaceId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.getSimpleExtentNDims: \n"
	    "  Failed to get number of dimentsions\n"
	    "    dataspace id = {0}\n"
            "    status = {1}\n",
	    spaceId->Id, status);
	 throw gcnew H5SgetSimpleExtentNDimsException(message, status);
      }
      return status;
   }
   
   bool H5S::selectNone(H5DataSpaceId^ spaceId)
   {
      hid_t status = H5Sselect_none(spaceId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5S.selectNone: \n"
	    "  Failed with\n"
	    "    dataspace id = {0}\n"
            "    status = {1}\n",
	    spaceId->Id, status);
	 throw gcnew H5SselectNoneException(message, status);
      }
      return (status != 0);
   }
   
   
   array<hsize_t>^ H5S::getSimpleExtentMaxDims(H5DataSpaceId^ spaceId)
   {
      array<hsize_t>^ extent = gcnew array<hsize_t>(H5S_MAX_RANK);
      array<hsize_t>^ maxExtent = gcnew array<hsize_t>(H5S_MAX_RANK);

      // Find the extents
      hid_t nDimensions = H5Sget_simple_extent_dims(
	 spaceId->Id,
	 extent,
	 maxExtent);
      
      // If nDimensions is not valid
      if ((nDimensions < 0) || (nDimensions > H5S_MAX_RANK))
      {
	 String^ message = String::Format(
	    "H5S.getSimpleExtentMaxDims: \n"
	    "  Failed to get max extent with status {0}\n",
	    nDimensions);

	 throw gcnew H5SgetSimpleExtentMaxDimsException(message, nDimensions);
      }


      // Copy the extents to the return array
      array<hsize_t>^ returnExtent = gcnew array<hsize_t>(nDimensions);
      
      for(int i=0;i<nDimensions;i++)
      {
	 returnExtent[i] = maxExtent[i];
      }

      return returnExtent;
   }
   
   array<hsize_t>^ H5S::getSimpleExtentDims(H5DataSpaceId^ spaceId)
   {
      array<hsize_t>^ extent = gcnew array<hsize_t>(H5S_MAX_RANK);
      array<hsize_t>^ maxExtent = gcnew array<hsize_t>(H5S_MAX_RANK);

      // Find the extents
      hid_t nDimensions = H5Sget_simple_extent_dims(
	 spaceId->Id,
	 extent,
	 maxExtent);
      
      // If nDimensions is not valid
      if ((nDimensions < 0) || (nDimensions > H5S_MAX_RANK))
      {
	 String^ message = String::Format(
	    "H5S.getSimpleExtentDims: \n"
	    "  Failed to get extent with status {0}\n",
	    nDimensions);

	 throw gcnew H5SgetSimpleExtentDimsException(message, nDimensions);
      }

      // Copy the extents to the return array
      array<hsize_t>^ returnExtent = gcnew array<hsize_t>(nDimensions);
      
      for(int i=0;i<nDimensions;i++)
      {
	 returnExtent[i] = extent[i];
      }

      return returnExtent;
   }
   
 

}
