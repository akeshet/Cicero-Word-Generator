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
#include <cassert>
using namespace std;

using namespace System;
using namespace System::Text;
using namespace System::Runtime::InteropServices;

#include "H5T.h"
#include "H5Common.h"

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Tcopy(hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" hid_t _cdecl H5Tset_size(hid_t typeId, size_t size);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tclose(hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" size_t _cdecl H5Tget_size(hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" H5T_sign_t _cdecl H5Tget_sign(hid_t typeId);


[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tset_order(hid_t typeId,
				      H5T_order_t order);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Topen(hid_t groupOrFileId,
                              [MarshalAs(UnmanagedType::LPStr)] 
			      String^ datatypeName);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tcreate(H5T_class_t type, size_t size);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tenum_create(hid_t type);


[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tinsert(hid_t compoundTypeId,
				   [MarshalAs(UnmanagedType::LPStr)] 
				   String^ fieldName,
                                   size_t offset,
                                   hid_t filedId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tcommit(hid_t location,
				   [MarshalAs(UnmanagedType::LPStr)] 
				   String^ dataTypeName,
				   hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tget_native_type(hid_t typeId,
					    H5T_direction_t offset);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tvlen_create(hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tget_class(hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Tget_nmembers(hid_t typeId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Tget_member_index(hid_t typeId,
				       [MarshalAs(UnmanagedType::LPStr)] 
				       String^ fieldName);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Tenum_insert(hid_t typeId,
				       [MarshalAs(UnmanagedType::LPStr)] 
					String^ name,
                                        void* value);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Tget_member_class(hid_t typeId,
					  unsigned int memberNumber);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" int _cdecl H5Tget_member_type(hid_t typeId,
					 unsigned int fieldIndex);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" char* _cdecl H5Tget_member_name(hid_t typeId,
					   unsigned int fieldIndex);
#pragma unmanaged
   extern "C" void free(void* ptr);

   void H5UnmanagedFree(char* ptr)
   {
	   free(ptr);
   }
#pragma managed

namespace HDF5DotNet
{
   hid_t H5T::getStdType(H5T::H5Type stdType)
   {
      // Set the data type
      hid_t typeFlag;
      switch(stdType)
      {
        // I8LE
	 case H5Type::STD_I8LE:
	    typeFlag = H5T_STD_I8LE_g;
	    break;
	    
	    // I16BE
	 case H5Type::STD_I16BE:
	    typeFlag = H5T_STD_I16BE_g;
	    break;
	    
	    // I16LE
	  case H5Type::STD_I16LE:
	    typeFlag = H5T_STD_I16LE_g;
	    break;
	    
	    // I32BE
	  case H5Type::STD_I32BE:
	    typeFlag = H5T_STD_I32BE_g;
	    break;
	    
	    // I32LE
	  case H5Type::STD_I32LE:
	    typeFlag = H5T_STD_I32LE_g;
	    break;
	    
	    // I64BE
	  case H5Type::STD_I64BE:
	    typeFlag = H5T_STD_I64BE_g;
	    break;
	 
	    // I64LE
	  case H5Type::STD_I64LE:
	    typeFlag = H5T_STD_I64LE_g;
	    break;
	 
	    // U8BE
	  case H5Type::STD_U8BE:
	    typeFlag = H5T_STD_U8BE_g;
	    break;
	    
      	    // U8LE
	  case H5Type::STD_U8LE:
	    typeFlag = H5T_STD_U8LE_g;
	    break;
	    
      	    // U16BE
	  case H5Type::STD_U16BE:
	    typeFlag = H5T_STD_U16BE_g;
	    break;
	    
      	    // U16LE
	  case H5Type::STD_U16LE:
	    typeFlag = H5T_STD_U16LE_g;
	    break;
	    
      	    // U32BE
	  case H5Type::STD_U32BE:
	    typeFlag = H5T_STD_U32BE_g;
	    break;
	    
      	    // U32LE
	  case H5Type::STD_U32LE:
	    typeFlag = H5T_STD_U32LE_g;
	    break;
	    
      	    // U64BE
	  case H5Type::STD_U64BE:
	    typeFlag = H5T_STD_U64BE_g;
	    break;
	    
      	    // U64LE
	  case H5Type::STD_U64LE:
	    typeFlag = H5T_STD_U64LE_g;
	    break;
	    
      	    // B8BE
	  case H5Type::STD_B8BE:
	    typeFlag = H5T_STD_B8BE_g;
	    break;
	    
      	    // B8LE
	  case H5Type::STD_B8LE:
	    typeFlag = H5T_STD_B8LE_g;
	    break;
	    
      	    // B16BE
	  case H5Type::STD_B16BE:
	    typeFlag = H5T_STD_B16BE_g;
	    break;
	    
      	    // B16LE
	  case H5Type::STD_B16LE:
	    typeFlag = H5T_STD_B16LE_g;
	    break;
	    
      	    // B32BE
	  case H5Type::STD_B32BE:
	    typeFlag = H5T_STD_B32BE_g;
	    break;
	    
      	    // B32LE
	  case H5Type::STD_B32LE:
	    typeFlag = H5T_STD_B32LE_g;
	    break;
	    
      	    // B64BE
	  case H5Type::STD_B64BE:
	    typeFlag = H5T_STD_B64BE_g;
	    break;
	    
      	    // B64LE
	  case H5Type::STD_B64LE:
	    typeFlag = H5T_STD_B64LE_g;
	    break;
	    
      	    // REF_OBJ
	  case H5Type::STD_REF_OBJ:
	    typeFlag = H5T_STD_REF_OBJ_g;
	    break;
	    
      	    // REF_DSETREG
	  case H5Type::STD_REF_DSETREG:
	    typeFlag = H5T_STD_REF_DSETREG_g;
	    break;

	 case H5Type::NATIVE_SCHAR:
	    typeFlag = H5T_NATIVE_SCHAR_g;
	    break;

	 case H5Type::NATIVE_UCHAR:
	    typeFlag = H5T_NATIVE_UCHAR_g;
	    break;

	 case H5Type::NATIVE_SHORT:
	    typeFlag = H5T_NATIVE_SHORT_g;
	    break;

	 case H5Type::NATIVE_USHORT:
	    typeFlag = H5T_NATIVE_USHORT_g;
	    break;

	 case H5Type::NATIVE_INT:
	    typeFlag = H5T_NATIVE_INT_g;
	    break;
	    
	 case H5Type::NATIVE_UINT:
	    typeFlag = H5T_NATIVE_UINT_g;
	    break;
	    
	 case H5Type::NATIVE_LONG:
	    typeFlag = H5T_NATIVE_LONG_g;
	    break;
	    
	 case H5Type::NATIVE_ULONG:
	    typeFlag = H5T_NATIVE_ULONG_g;
	    break;
	    
	 case H5Type::NATIVE_LLONG:
	    typeFlag = H5T_NATIVE_LLONG_g;
	    break;
	    
	 case H5Type::NATIVE_ULLONG:
	    typeFlag = H5T_NATIVE_ULLONG_g;
	    break;
	    
	 case H5Type::NATIVE_FLOAT:
	    typeFlag = H5T_NATIVE_FLOAT_g;
	    break;
	    
	 case H5Type::NATIVE_DOUBLE:
	    typeFlag = H5T_NATIVE_DOUBLE_g;
	    break;
	    
	 case H5Type::NATIVE_LDOUBLE:
	    typeFlag = H5T_NATIVE_LDOUBLE_g;
	    break;
	    
	 case H5Type::NATIVE_B8:
	    typeFlag = H5T_NATIVE_B8_g;
	    break;
	    
	 case H5Type::NATIVE_B16:
	    typeFlag = H5T_NATIVE_B16_g;
	    break;
	    
	 case H5Type::NATIVE_B32:
	    typeFlag = H5T_NATIVE_B32_g;
	    break;

	 case H5Type::NATIVE_B64:
	    typeFlag = H5T_NATIVE_B64_g;
	    break;
	    
	 case H5Type::NATIVE_OPAQUE:
	    typeFlag = H5T_NATIVE_OPAQUE_g;
	    break;
	    
	 case H5Type::NATIVE_HADDR:
	    typeFlag = H5T_NATIVE_HADDR_g;
	    break;
	    
	 case H5Type::NATIVE_HSIZE:
	    typeFlag = H5T_NATIVE_HSIZE_g;
	    break;
	    
	 case H5Type::NATIVE_HSSIZE:
	    typeFlag = H5T_NATIVE_HSSIZE_g;
	    break;
	    
	 case H5Type::NATIVE_HERR:
	    typeFlag = H5T_NATIVE_HERR_g;
	    break;
	    
	 case H5Type::NATIVE_HBOOL:
	    typeFlag = H5T_NATIVE_HBOOL_g;
	    break;

         case H5Type::IEEE_F32LE:
	    typeFlag = H5T_IEEE_F32LE_g;
	    break;

         case H5Type::IEEE_F32BE:
	    typeFlag = H5T_IEEE_F32BE_g;
	    break;

         case H5Type::IEEE_F64LE:
	    typeFlag = H5T_IEEE_F64LE_g;
	    break;

	 case H5Type::IEEE_F64BE:
	    typeFlag = H5T_IEEE_F64BE_g;
	    break;

	case H5Type::C_S1:
	    typeFlag = H5T_C_S1_g;
	    break;

	 default:
	    // All members of the enumeration class should have their own case.
	    assert(0);
      }
      return typeFlag;
   }

   H5DataTypeId^ H5T::copy(H5T::H5Type stdType)
   {
      herr_t status = H5Tcopy(H5T::getStdType(stdType));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.copy: \n"
	    "  Failed to copy {0} with status {1}\n",
	    stdType, status);
	 throw gcnew H5TcopyException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }

   H5DataTypeId^ H5T::open(H5LocId^ groupOrFileId,
			   String^ datatypeName)
   {
      herr_t status = H5Topen(groupOrFileId->Id,
				datatypeName);
      
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.open: \n"
	    "  Failed to open {0} in {1} with status {2}\n",
	    datatypeName, groupOrFileId->Id, status);
	 throw gcnew H5TopenException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }
   
   H5DataTypeId^ H5T::copy(H5DataTypeId^ typeId)
   {
      herr_t status = H5Tcopy(typeId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.copy: \n"
	    "  Failed to copy {0} with status {1}\n",
	    typeId->Id, status);
	 throw gcnew H5TcopyException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }
   
   H5DataTypeId^ H5T::copy(H5DataSetId^ dataSetId)
   {
      herr_t status = H5Tcopy(dataSetId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.copy: \n"
	    "  Failed to copy {0} with status {1}\n",
	    dataSetId->Id, status);
	 throw gcnew H5TcopyException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }
   
   void H5T::setSize(H5DataTypeId^ typeId, size_t size)
   {
      herr_t status = H5Tset_size(typeId->Id, size);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.setSize: \n"
	    "  Failed to set_size of {0} with status {1}\n",
	    typeId->Id, status);
	 throw gcnew H5TsetSizeException(message, status);
      }
   }

   void H5T::close(H5DataTypeId^ typeId)
   {
      herr_t status = H5Tclose(typeId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.close: \n"
	    "  Failed to close type {0} with status {1}\n",
	    typeId->Id, status);
	 throw gcnew H5TcloseException(message, status);
      }
   }

   H5T::Sign H5T::getSign(H5DataTypeId^ typeId)
   {
      H5T_sign_t status = H5Tget_sign(typeId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getSign: \n"
	    "  Failed to get sign of {0} with status {1}\n",
	    typeId->Id, safe_cast<int>(status));
	 throw gcnew H5TgetSignException(message, status);
      }

      return H5T::Sign(status);
   }
   
   H5T::Sign H5T::getSign(H5T::H5Type h5Type)
   {
      H5T_sign_t status = H5Tget_sign(getStdType(h5Type));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getSign: \n"
	    "  Failed to get sign of {0} with status {1}\n",
	    h5Type, safe_cast<int>(status));
	 throw gcnew H5TgetSignException(message, safe_cast<int>(status));
      }

      return H5T::Sign(status);
   }
   

   herr_t H5T::setOrder(H5DataTypeId^ typeId, 
		 Order order)
   {
      herr_t status = H5Tset_order(typeId->Id, 
				   safe_cast<H5T_order_t>(order));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.setOrder: \n"
	    "  Failed to set order of typeId {0} to {1} with status {2}\n",
	    typeId->Id, order, status);
	 throw gcnew H5TsetOrderException(message, status);
      }
      
      return status;
   }
   
   size_t H5T::getSize(H5DataTypeId^ typeId)
   {
      size_t status = H5Tget_size(typeId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.setOrder: \n"
	    "  Failed to get size of typeId {0} with status {1}\n",
	    typeId->Id, status);
	 throw gcnew H5TgetSizeException(message, status);
      }
      
      return status;
   }

   size_t H5T::getSize(H5T::H5Type h5Type)
   {
      size_t status = H5Tget_size(getStdType(h5Type));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.setOrder: \n"
	    "  Failed to get size of type {0} with status {1}\n",
	    h5Type, status);
	 throw gcnew H5TgetSizeException(message, status);
      }
      
      return status;
   }

   H5DataTypeId^ H5T::create(H5T::CreateClass createClass,
			     size_t size)
   {
      herr_t status = H5Tcreate(safe_cast<H5T_class_t>(createClass), size);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.create: \n"
	    "  Failed to create type of {0} with {1} bytes - status {2}\n",
	    createClass, size, status);
	 throw gcnew H5TcreateException(message, status);
      }
      
      return gcnew H5DataTypeId(status);
   }

   H5DataTypeId^ H5T::enumCreate(H5T::H5Type h5Type)
   {
      herr_t status = H5Tenum_create(getStdType(h5Type));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.enumCreate: \n"
	    "  Failed to create type with {0} as parent - status {2}\n",
	    h5Type, status);
	 throw gcnew H5TenumCreateException(message, status);
      }
      
      return gcnew H5DataTypeId(status);
   }
   
			       
   H5DataTypeId^ H5T::enumCreate(H5DataTypeId^ parentId)
   {
      herr_t status = H5Tenum_create(parentId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.enumCreate: \n"
	    "  Failed to create type with {0} as parent - status {2}\n",
	    parentId->Id, status);
	 throw gcnew H5TenumCreateException(message, status);
      }
      
      return gcnew H5DataTypeId(status);
   }

   void H5T::insert(H5DataTypeId^ compoundDataType,
		    String^ fieldName, 
		    size_t offset,
		    H5T::H5Type h5Type)
   {
      H5T::insert(compoundDataType,
		  fieldName,
		  offset,
		  gcnew H5DataTypeId(getStdType(h5Type)));
   }
   
   void H5T::insert(H5DataTypeId^ compoundDataType,
		    String^ fieldName, 
		    size_t offset,
		    H5DataTypeId^ fieldId)
   {
      herr_t status = H5Tinsert(compoundDataType->Id,
	                        fieldName,
	                        offset,
                                fieldId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.insert: \n"
	    "  Failed to insert field name {0} into compound datatype with "
            " id {1}, offset of {2}, and fieldId of {3} - status is {4}\n",
	    fieldName, compoundDataType->Id, offset, fieldId->Id, status);
	 throw gcnew H5TinsertException(message, status);
      }
   }

   H5DataTypeId^ H5T::getNativeType(H5DataTypeId^ typeId,
				    H5T::Direction direction)
   {
      herr_t status = H5Tget_native_type(typeId->Id,
					 safe_cast<H5T_direction_t>(direction));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getNativeType: \n"
	    "  Failed to get native type for type id {0} with "
            " direction {1} - status is {2}\n",
	    typeId->Id, direction, status);
	 throw gcnew H5TgetNativeTypeException(message, status);
      }

      return gcnew H5DataTypeId(status);
      
   }

   H5DataTypeId^ H5T::getNativeType(H5T::H5Type h5Type,
				      H5T::Direction direction)
   {
	   herr_t status = H5Tget_native_type(H5T::getStdType(h5Type),
					 safe_cast<H5T_direction_t>(direction));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getNativeType: \n"
	    "  Failed to get native type {0} with "
            " direction {1} - status is {2}\n",
	    h5Type, direction, status);
	 throw gcnew H5TgetNativeTypeException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }

   H5DataTypeId^ H5T::vlenCreate(H5T::H5Type h5Type)
   {
      herr_t status = H5Tvlen_create(getStdType(h5Type));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.vlenCreate: \n"
	    "  Failed to create type of {0} with status {1}",
	    h5Type, status);
	 throw gcnew H5TvlenCreateException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }
   
   H5DataTypeId^ H5T::vlenCreate(H5DataTypeId^ baseId)
   {
      herr_t status = H5Tvlen_create(baseId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.vlenCreate: \n"
	    "  Failed to create type for type id {0} with status {1}",
	    baseId->Id, status);
	 throw gcnew H5TvlenCreateException(message, status);
      }

      return gcnew H5DataTypeId(status);
   }

   
   H5T::H5TClass H5T::getClass(H5DataTypeId^ typeId)
   {
      herr_t status = H5Tget_class(typeId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getClass: \n"
	    "  Failed to get class for type id {0} with status {1}",
	    typeId->Id, status);
	 throw gcnew H5TgetClassException(message, status);
      }
      return H5T::H5TClass(status);
   }

   H5T::H5TClass H5T::getClass(H5T::H5Type h5Type)
   {
      herr_t status = H5Tget_class(getStdType(h5Type));

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getClass: \n"
	    "  Failed to get class for type {0} with status {1}",
	    h5Type, status);
	 throw gcnew H5TgetClassException(message, status);
      }
      return H5T::H5TClass(status);
   }


   int H5T::getMemberIndex(H5DataTypeId^ typeId,
		      String^ fieldName)
   {
      int status = H5Tget_member_index(typeId->Id, fieldName);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getMemberIndex: \n"
	    "  Failed to get index for fieldName {0},  type id {1} with "
            "status {2}",
	    fieldName, typeId->Id, status);
	 throw gcnew H5TgetMemberIndexException(message, status);
      }
      return status;
   }
   
   H5DataTypeId^ H5T::getMemberType(H5DataTypeId^ typeId,
				    unsigned int fieldIndex)
   {
      int status = H5Tget_member_type(typeId->Id, fieldIndex);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getMemberType: \n"
	    "  Failed to get type for fieldIndex {0},  type id {1} with "
            "status {2}",
	    fieldIndex, typeId->Id, status);
	 throw gcnew H5TgetMemberTypeException(message, status);
      }
      return gcnew H5DataTypeId(status);
   }

   String^ H5T::getMemberName(H5DataTypeId^ typeId,
			      unsigned int fieldIndex)
   {
      char* name = H5Tget_member_name(typeId->Id, fieldIndex);

      if (name == 0)
      {
	 String^ message = String::Format(
	    "H5T.getMemberName: \n"
	    "  Failed to get name for fieldIndex {0},  type id {1} ",
	    fieldIndex, typeId->Id);
	 throw gcnew H5TgetMemberNameException(message, 0);
      }

      String^ memberName = gcnew String(name);
      //H5UnmanagedFree(name);
      
      return memberName;
   }
   

   int H5T::getNMembers(H5DataTypeId^ typeId)
   {
      int status = H5Tget_nmembers(typeId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getNMembers: \n"
	    "  Failed to get class for type id {0} with status {1}",
	    typeId->Id, status);
	 throw gcnew H5TgetNMembersException(message, status);
      }
      return status;
   }
   
   H5T::H5TClass H5T::getMemberClass(H5DataTypeId^ typeId, 
				       unsigned int memberNumber)
   {
      int status = H5Tget_member_class(typeId->Id, memberNumber);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.getMemberClass: \n"
	    "  Failed to get class for member number {0} of type id {1} "
            "with status {2}",
	    memberNumber, typeId->Id, status);
	 throw gcnew H5TgetMemberClassException(message, status);
      }
      return H5T::H5TClass(status);
   }

   generic <class Type>
   void H5T::enumInsert(H5DataTypeId^ typeId,
                        String^ name,
			Type% value)
   {
      pin_ptr<Type> ptr = &value;
      herr_t status = H5Tenum_insert(typeId->Id,
				     name,
				     ptr);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.enumInsert: \n"
	    "  Failed to insert:\n"
	    "     location ID: {0}\n"
	    "     name: {1}\n"
        "     status: {2}\n", 
	    typeId->Id, name, status);

	 throw gcnew H5TenumInsertException(message, status);
      }
   }
   
   void H5T::commit(H5LocId^ location, 
		    String^ dataTypeName,
		    H5DataTypeId^ typeId)
   {
      herr_t status = H5Tcommit(location->Id,
				dataTypeName,
				typeId->Id);
      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5T.commit: \n"
	    "  Failed to commit:\n"
	    "     location ID: {0}\n"
	    "     dataTypeName: {1}"
	    "     type ID: {3}" 
	    "     with status {4}",
	    location->Id, dataTypeName, typeId->Id, status );

	 throw gcnew H5TcommitException(message, status);
      }
   }

}

	 
	 
