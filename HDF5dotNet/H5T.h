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
//#include "H5Common.h"
#include "HDFException.h"
#include "HDFExceptionSubclasses.h"

#define _HDF5USEDLL_
//#define H5_DLLVAR extern "C" __declspec(dllimport)
#define HDF5_DOT_NET
#include "H5Tpublic.h"

namespace HDF5DotNet
{
   /// <summary>
   /// The H5D contains static member function for each of the supported
   /// H5D calls of the HDF5 library.
   /// </summary>
   ref class H5DataTypeId;
   ref class H5FileId;
   ref class H5LocId;
   ref class H5DataSetId;

   public ref class H5T
   {
      public:

	 // sandcastle can't handle the inheritance
	 //enum class Order: unsigned int
	 enum class Order
	 {
	       ///<summary> Little endian </summary>
	       LE = H5T_ORDER_LE,

	       ///<summary> Big endian </summary>
	       BE = H5T_ORDER_BE
	 };

	 enum class Sign
	 {
	       ///<summary> Unsigned number </summary>
	       UNSIGNED = H5T_SGN_NONE,

	       ///<summary> Two's complement  number </summary>
	       TWOS_COMPLEMENT = H5T_SGN_2
	 };
	 

	 enum class Direction
	 {
	       ///<summary> The default direction is ascending. </summary>
	       DEFAULT = H5T_DIR_DEFAULT,

	       ///<summary> Ascending order </summary>
               ASCEND = H5T_DIR_ASCEND,

	       ///<summary> Descending order </summary>
	       DESCEND = H5T_DIR_DESCEND
	 };
	 
	    
	 // sandcastle can't handle the inheritance
	 //enum class H5Type: unsigned int
	 enum class H5Type
	 {
	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_I8BE,

	    ///<summary> 8-bit signed integer (little endian)    </summary>
	    STD_I8LE,

	    ///<summary> 16-bit signed integer (big endian)    </summary>
	    STD_I16BE,

	    ///<summary> 16-bit signed integer (little endian)    </summary>
	    STD_I16LE,

	    ///<summary> 32-bit signed integer (big endian)    </summary>
	    STD_I32BE,

	    ///<summary> 32-bit signed integer (little endian)    </summary>
	    STD_I32LE,

	    ///<summary> 64-bit signed integer (big endian)    </summary>
	    STD_I64BE,

	    ///<summary> 64-bit signed integer (little endian)    </summary>
	    STD_I64LE,

	    ///<summary> 8-bit unsigned integer (big endian)    </summary>
	    STD_U8BE,

	    ///<summary> 8-bit unsigned integer (little endian)    </summary>
	    STD_U8LE,

	    ///<summary> 16-bit unsigned integer (big endian)    </summary>
	    STD_U16BE,

	    ///<summary> 16-bit unsigned integer (little endian)    </summary>
	    STD_U16LE,

	    ///<summary> 32-bit unsigned integer (big endian)    </summary>
	    STD_U32BE,

	    ///<summary> 32-bit unsigned integer (little endian)    </summary>
	    STD_U32LE,

	    ///<summary> 64-bit unsigned integer (big endian)    </summary>
	    STD_U64BE,

	    ///<summary> 64-bit unsigned integer (little endian)    </summary>
	    STD_U64LE,

	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_B8BE,

	    ///<summary> 8-bit signed integer (little endian)    </summary>
	    STD_B8LE,

	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_B16BE,

	    ///<summary> 8-bit signed integer (little endian)    </summary>
	    STD_B16LE,

	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_B32BE,

	    ///<summary> 8-bit signed integer (little endian)    </summary>
	    STD_B32LE,

	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_B64BE,

	    ///<summary> 8-bit signed integer (little endian)    </summary>
	    STD_B64LE,

	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_REF_OBJ,

	    ///<summary> 8-bit signed integer (big endian)    </summary>
	    STD_REF_DSETREG,
		
	    /// <summary> Native signed character </summary>
            NATIVE_SCHAR,

	    /// <summary> Native unsigned character </summary>
            NATIVE_UCHAR,

	    /// <summary> Native signed short </summary>
            NATIVE_SHORT,

	    /// <summary> Native unsigned short </summary>
            NATIVE_USHORT,

	    /// <summary> Native signed int </summary>
            NATIVE_INT,

	    /// <summary> Native unsigned int </summary>
            NATIVE_UINT,

	    /// <summary> Native signed long </summary>
            NATIVE_LONG,

	    /// <summary> Native unsigned long </summary>
            NATIVE_ULONG,

	    /// <summary> Native signed long long </summary>
            NATIVE_LLONG,

	    /// <summary> Native unsigned long long </summary>
            NATIVE_ULLONG,

	    /// <summary> Native float </summary>
            NATIVE_FLOAT,

	    /// <summary> Native double </summary>
            NATIVE_DOUBLE,

	    /// <summary> Native long double </summary>
            NATIVE_LDOUBLE,

	    /// <summary> Native 8-bit bitfield </summary>
            NATIVE_B8,

	    /// <summary> Native 16-bit bitfield </summary>
            NATIVE_B16,

	    /// <summary> Native 32-bit bitfield </summary>
            NATIVE_B32,

	    /// <summary> Native 64-bit bitfield </summary>
            NATIVE_B64,

	    /// <summary> Native opaque </summary>
            NATIVE_OPAQUE,

	    /// <summary> Native haddr</summary>
            NATIVE_HADDR,

	    /// <summary> Native hsize </summary>
            NATIVE_HSIZE,

	    /// <summary> Native hssize</summary>
            NATIVE_HSSIZE,

	    /// <summary> Native herr</summary>
            NATIVE_HERR,

	    /// <summary> Native hbool</summary>
            NATIVE_HBOOL,

	    /// <summary> IEEE 32-bit Floating (little endian) </summary>
            IEEE_F32LE,

            /// <summary> IEEE 32-bit Floating (big endian) </summary>
            IEEE_F32BE,

            /// <summary> IEEE 64-bit Floating (little endian) </summary> 
            IEEE_F64LE,

            /// <summary> IEEE 64-bit Floating (big endian) </summary>
            IEEE_F64BE,

            /// <summary> IEEE 64-bit Floating (big endian) </summary>
            C_S1 
	 };

	 /// <summary> Allowable data types for H5TCreate </summary>
	 enum class CreateClass
	 {
	    /// <summary> Compound Data Type </summary>
	    COMPOUND = H5T_COMPOUND,

	    /// <summary> Opaque Data Type </summary>
	    OPAQUE = H5T_OPAQUE,
	    
	    /// <summary> Enum Data Type </summary>
	    ENUM = H5T_ENUM
	 };
	    

	 enum class H5TClass 
	 {
	    /// <summary> Integer class </summary>
            INTEGER = H5T_INTEGER,

	    /// <summary> Float class </summary>
            FLOAT = H5T_FLOAT,

	    /// <summary> Time class </summary>
            TIME = H5T_TIME,

	    /// <summary> String class (fixed or variable length) </summary>
            STRING = H5T_STRING,

	    /// <summary> Bitfield class </summary>
            BITFIELD = H5T_BITFIELD,

	    /// <summary> Opaque class </summary>
            OPAQUE = H5T_OPAQUE,

	    /// <summary> Compound class </summary>
            COMPOUND = H5T_COMPOUND,

	    /// <summary> Reference class </summary>
            REFERENCE = H5T_REFERENCE,

	    /// <summary> Enum class </summary>
            ENUM = H5T_ENUM,

	    /// <summary> Vlen class </summary>
            VLEN = H5T_VLEN,

	    /// <summary> Array class </summary>
            ARRAY = H5T_ARRAY
	 };
	    

	 /// <summary> 
	 /// H5T.open opens a named datatype at the location specified by
         /// groupOrFileId and returns an identifier for the datatype. 
	 /// groupOrFileId is
         /// either a file or group identifier. The identifier should
         /// eventually be closed by calling H5Tclose to release resources.
         /// </summary>
	 /// <param name="groupOrFileId"> 
	 /// </param>
	 ///  	IN: A file or group identifier.
	 /// <param name="datatypeName"> 
	 ///  	IN: A datatype name, defined within the file or group
	 /// identified by groupOrFileId.
	 /// </param>
         /// <exception cref="H5TopenException"> throws H5TopenException 
	 /// when open fails
         /// </exception>
	 /// <returns> a vaild H5DataTypeId for the opened data type 
	 /// </returns>
	 static H5DataTypeId^ open(H5LocId^ groupOrFileId,
				   String^ datatypeName);

	 /// <summary> 
	 /// H5T.create creates a new data type of the specified class with
	 /// the specified number of bytes.
	 /// </summary>
	 /// <param name="createClass">
	 ///  Available create classes include COMPOUND, OPAQUE, and ENUM.
	 /// </param>
	 /// <param name="size">
	 /// Number of bytes in the created data type.
	 /// </param>
         /// <exception cref="H5TopenException"> throws H5TopenException 
	 /// when open fails
         /// </exception>
	 /// <remarks>
	 /// Use H5Tcopy to create integer or floating point data types.
	 /// </remarks>
	 /// <returns> a vaild H5DataTypeId for the created data type 
	 /// </returns>
	 static H5DataTypeId^ create(H5T::CreateClass createClass,
				     size_t size);
	 

	 static H5DataTypeId^ copy(H5T::H5Type stdType);
	 static H5DataTypeId^ copy(H5DataTypeId^ typeId);
	 static H5DataTypeId^ copy(H5DataSetId^ dataSetId);
	 
	 static void setSize(H5DataTypeId^ typeId, size_t size);

	 static void close(H5DataTypeId^ typeId);

	 static herr_t setOrder(H5DataTypeId^ typeId, 
		 H5T::Order order);
	 
	 /// <summary> 
         /// getSize returns the size of a datatype in bytes.
         /// </summary>
	 /// <param name="typeId"> 
	 ///  	IN: Identifier of datatype to query.
	 /// </param>
         /// <exception cref="H5TgetSizeException"> 
	 /// throws H5TgetSizeException on failure.
         /// </exception>
	 /// <returns> 
	 /// Returns the size of the datatype in bytes if successful.
	 /// </returns>
	 static size_t getSize(H5DataTypeId^ typeId);
	 static size_t getSize(H5T::H5Type h5Type);

	 /// <summary> 
         /// </summary>
	 /// <param name="typeId"> 
	 ///  	IN: Identifier of datatype to query.
	 /// </param>
         /// <exception cref="H5TgetSizeException"> 
	 /// throws H5TgetSignException on failure.
         /// </exception>
	 /// <returns> 
	 /// Returns an H5T.Sign that indicates the sign.  Possible results
	 /// include H5T.Sign.UNSIGNED and H5T.Sign.TWOS_COMPLEMENT
	 /// </returns>
	 static H5T::Sign getSign(H5DataTypeId^ typeId);
	 static H5T::Sign getSign(H5T::H5Type h5Type);

	 /// <summary>
	 /// H5T.enumCreate creates a new enumeration datatype based on the
	 /// specified base datatype parent_id, which must be an integer type. 
	 /// </summary>
	 /// <param name="parentId"> IN: Datatype identifier for the 
	 /// base type. This must be an integer type.
	 /// </param>
	 /// <returns>
	 /// Returns the datatype identifier for the new enumeration
	 /// datatype if successful.
	 /// </returns>
	 /// <exception cref="H5TenumCreateException">
	 /// throws H5TenumCreateException on failure.
	 /// </exception>
	 static H5DataTypeId^ enumCreate(H5DataTypeId^ parentId);

	 /// <summary>
	 /// H5T.enumCreate creates a new enumeration datatype based on the
	 /// specified base datatype parent_id, which must be an integer type. 
	 /// </summary>
	 /// <param name="h5Type"> IN: Datatype identifier for the 
	 /// base type.
	 /// </param>
	 /// <returns>
	 /// Returns the datatype identifier for the new enumeration
	 /// datatype if successful.
	 /// </returns>
	 /// <exception cref="H5TenumCreateException">
	 /// throws H5TenumCreateException on failure.
	 /// </exception>
	 static H5DataTypeId^ enumCreate(H5T::H5Type h5Type);


	 /// <summary>
	 /// H5Tinsert adds another member to the compound datatype
	 /// type_id. The new member has a name which must be unique within
	 /// the compound datatype. The offset argument defines the start
	 /// of the member in an instance of the compound datatype, and
	 /// field_id is the datatype identifier of the new member.
	 /// </summary>
	 /// <param name="compoundDataType"> Identifier of compound data
	 /// type to modify.
	 /// </param>
	 /// <param name="fieldName">
	 /// Name of the field to insert.
	 /// </param>
	 /// <param name="offset">
	 /// Offset in memory structure of the field to insert.
	 /// </param>
	 /// <param name="fieldId">
	 /// Datatype identifier of the field to insert.
	 /// </param>
	 /// <exception cref="H5TinsertException">
	 /// throws H5TinsertException on failure.
	 /// </exception>
	 /// <remarks>
	 /// Members of a compound datatype do not have to be atomic
	 /// datatypes; a compound datatype can have a member which is a
	 /// compound datatype.
	 /// </remarks>
	 static void insert(H5DataTypeId^ compoundDataType,
				   String^ fieldName, 
	                           size_t offset,
	                           H5DataTypeId^ fieldId);


	 /// <summary>
	 /// H5Tinsert adds another member to the compound datatype
	 /// type_id. The new member has a name which must be unique within
	 /// the compound datatype. The offset argument defines the start
	 /// of the member in an instance of the compound datatype, and
	 /// field_id is the datatype identifier of the new member.
	 /// </summary>
	 /// <param name="compoundDataType"> Identifier of compound data
	 /// type to modify.
	 /// </param>
	 /// <param name="fieldName">
	 /// Name of the field to insert.
	 /// </param>
	 /// <param name="offset">
	 /// Offset in memory structure of the field to insert.
	 /// </param>
	 /// <param name="fieldId">
	 /// Datatype identifier of the field to insert.
	 /// </param>
	 /// <exception cref="H5TinsertException">
	 /// throws H5TinsertException on failure.
	 /// </exception>
	 /// <remarks>
	 /// Members of a compound datatype do not have to be atomic
	 /// datatypes; a compound datatype can have a member which is a
	 /// compound datatype.
	 /// </remarks>
	 static void insert(H5DataTypeId^ compoundDataType,
			    String^ fieldName, 
			    size_t offset,
			    H5T::H5Type fieldId);

	 /// <summary>
	 /// Returns the native datatype of a specified datatype.
	 /// </summary>
	 /// <param name="typeId"> 
	 /// Datatype identifier for the dataset datatype.
	 /// </param>
	 /// <param name="direction">
	 /// Direction of search.
	 /// </param>
	 /// <exception cref="H5TgetNativeTypeException">
	 /// throws H5TgetNativeTypeException on failure.
	 /// </exception>
	 /// <remarks>
	 ///<p>
	 /// H5Tget_native_type returns the equivalent native datatype for
	 /// the datatype specified in type_id.
	 /// </p>
	 /// <p> H5Tget_native_type is a high-level function designed 
	 /// primarily to facilitate use of the H5Dread function, for which 
	 /// users otherwise must undertake a multi-step process to determine 
	 /// the native datatype of a dataset prior to reading it into memory. 
	 /// It can be used not only to determine the native datatype for 
	 /// atomic datatypes, but also to determine the native datatypes 
	 /// of the individual components of a compound datatype, an 
	 /// enumerated datatype, an array datatype, or a variable-length 
	 /// datatype. </p>
	 /// <p>
	 /// H5Tget_native_type selects the matching native datatype from
	 /// the following list:
	 /// </p>
	 /// <ul>
         /// <li>   H5T_NATIVE_CHAR   </li>
         /// <li>   H5T_NATIVE_SHORT        </li>
         /// <li>   H5T_NATIVE_INT          </li>
         /// <li>   H5T_NATIVE_LONG         </li>
         /// <li>   H5T_NATIVE_LLONG        </li>
         /// <li>   H5T_NATIVE_UCHAR </li>
         /// <li>   H5T_NATIVE_USHORT </li>
         /// <li>   H5T_NATIVE_UINT </li>
         /// <li>   H5T_NATIVE_ULONG </li>
         /// <li>   H5T_NATIVE_ULLONG </li>
         /// <li>   H5T_NATIVE_FLOAT </li>
         /// <li>   H5T_NATIVE_DOUBLE </li>
         /// <li>   H5T_NATIVE_LDOUBLE </li>
	 /// </ul>
	 ///<p>
    	 ///The direction parameter indicates the order in which the
    	 ///library searches for a native datatype match. Valid values for 
	 /// direction are as follows:
	 /// </p>
	 /// <ul>
	 /// <li>
         /// H5T_DIR_ASCEND searches the above list in ascending size of
         /// the datatype, i.e., from top to bottom. (Default) 
	 /// </li>
	 /// <li>
     	 /// H5T_DIR_DESCEND searches the above list in descending size of 
	 /// the datatype, i.e., from bottom to top.
	 /// </li>
	 /// </ul>
	 /// <p>
         /// H5Tget_native_type is designed primarily for use with intenger
         /// and floating point datatypes. Time, bifield, opaque, and 
	 /// reference datatypes are returned as a copy of type_id.
	 /// </p>
	 /// <p>
	 /// The identifier returned by H5Tget_native_type should
	 /// eventually be closed by calling H5Tclose to release resources. 
	 /// </p>
	 /// </remarks>
	 static H5DataTypeId^ getNativeType(H5DataTypeId^ typeId,
					    H5T::Direction direction);

	 static H5DataTypeId^ getNativeType(H5T::H5Type h5Type,
					    H5T::Direction direction);

	 /// <summary>
	 /// Creates a new variable-length datatype.
	 /// </summary>
	 /// <param name="baseId"> Base type of datatype to create.
	 /// </param>
	 /// <exception cref="H5TvlenCreateException">
	 /// throws H5TvlenCreateException on failure.
	 /// </exception>
	 /// <remarks>
	 /// <p>
    	 /// H5Tvlen_create creates a new variable-length (VL) datatype.
	 /// </p>
	 /// <p>
         ///    The base datatype will be the datatype that the sequence 
	 /// is composed of, characters for character strings, vertex 
	 /// coordinates for polygon lists, etc. The base type specified 
	 /// for the VL datatype can be of any HDF5 datatype, including 
	 /// another VL datatype, a compound datatype or an atomic datatype.
	 /// </p>
	 /// <p>
	 /// When necessary, use H5Tget_super to determine the base type of
	 /// the VL datatype.
	 /// </p>
	 /// <p>
	 /// The datatype identifier returned from this function 
	 /// should be released with H5Tclose or resource leaks will result. 
	 /// </p>
	 /// </remarks>
	 static H5DataTypeId^ vlenCreate(H5DataTypeId^ baseId);

	 static H5DataTypeId^ vlenCreate(H5T::H5Type h5Type);

	 /// <summary>
	 /// Returns the datatype class identifier.
	 /// </summary>
	 /// <param name="typeId"> Identifier of datatype to query.
	 /// </param>
	 /// <remarks>
	 /// <p>
    	 ///H5Tget_class returns the datatype class identifier.
	 /// </p>
	 /// <p>
	 /// Valid class identifiers, as defined in H5Tpublic.h, are:  
	 /// </p>
	 /// <ul>
	 /// <li> H5T_INTEGER </li>
         /// <li> H5T_FLOAT </li>
         /// <li> H5T_TIME </li>
         /// <li> H5T_STRING </li>
         /// <li> H5T_BITFIELD </li>
         /// <li> H5T_OPAQUE </li>
         /// <li> H5T_COMPOUND </li>
         /// <li> H5T_REFERENCE </li>
         /// <li> H5T_ENUM </li>
         /// <li> H5T_VLEN </li>
         /// <li> H5T_ARRAY  </li>
	 /// </ul> 
	 /// </remarks>
	 /// <exception cref="H5TgetClassException">
	 /// throws H5TgetClassException on failure.
	 /// </exception>
	 static H5TClass getClass(H5DataTypeId^ typeId);
	 static H5TClass getClass(H5T::H5Type h5Type);
	 
	 /// <summary>
	 /// Retrieves the index of a compound or enumeration datatype
	 /// member.
	 /// </summary>
	 /// <param name="typeId"> 
	 /// Identifier of datatype to query.
	 /// </param>
	 /// <returns>
	 /// Returns a valid field or member index if successful.
	 /// </returns>
	 /// <exception cref="H5TgetMemberIndexException">
	 /// throws H5TgetMemberIndexException on failure.
	 /// </exception>
	 /// <remarks>
	 /// <p>
         /// H5Tget_member_index retrieves the index of a field of a 
	 /// compound datatype or an element of an enumeration datatype.
	 /// </p>
	 /// <p>
	 /// The name of the target field or element is specified in fieldname.
	 /// </p>
	 /// <p>
	 /// Fields are stored in no particular order with index values 
	 /// of 0 through N-1, where N is the value returned by 
	 /// H5T.getNMembers.  
	 /// </p>
	 /// </remarks>
	 static int getMemberIndex(H5DataTypeId^ typeId,
				   String^ fieldName);

	 /// <summary>
	 /// Returns the datatype of the specified member.
	 /// </summary>
	 /// <param name="typeId"> 
	 /// Identifier of datatype to query.
	 /// </param>
	 /// <param name="fieldIndex"> 
	 /// Field index (0-based) of the field type to retrieve.
	 /// </param>
	 /// <returns>
	 /// Returns the identifier of a copy of the datatype of the field
	 /// if successful.
	 /// </returns>
	 /// <exception cref="H5TgetMemberTypeException">
	 /// throws H5TgetMemberTypeException on failure.
	 /// </exception>
	 /// <remarks>
	 /// H5Tget_member_type returns the datatype of the specified
	 /// member. The caller should invoke H5Tclose() to release
	 /// resources associated with the type.
	 /// </remarks>
	 static H5DataTypeId^ getMemberType(H5DataTypeId^ typeId,
					    unsigned int fieldIndex);
	 
	 /// <summary>
	 /// Retrieves the number of elements in a compound or enumeration 
	 /// datatype. 
	 /// </summary>
	 /// <param name="typeId"> 
	 /// Identifier of datatype to query.
	 /// </param>
	 /// <returns>
	 /// Returns the number of elements if successful.
	 /// </returns>
	 /// <exception cref="H5TgetNMembersException">
	 /// throws H5TgetNMembersException on failure.
	 /// </exception>
	 /// <remarks>
	 /// H5Tget_nmembers retrieves the number of fields in a compound
	 /// datatype or the number of members of an enumeration datatype.
	 /// </remarks>
	 static int getNMembers(H5DataTypeId^ typeId);

	 /// <summary>
	 /// Returns datatype class of compound datatype member.
	 /// </summary>
	 /// <param name="typeId"> 
	 /// IN: Datatype identifier of compound object.
	 /// </param>
	 /// <param name="memberNumber"> 
	 /// IN: Compound object member number.
	 /// </param>
	 /// <returns>
	 /// Returns the datatype class if successful.
	 /// </returns>
	 /// <exception cref="H5TgetMemberClassException">
	 /// throws H5TgetMemberClassException on failure.
	 /// </exception>
	 /// <remarks>
	 /// <p>
         /// Given a compound datatype, typeId, the function 
	 /// H5T.getMemberClass returns the datatype class of the compound
	 /// datatype member specified by memberNumber.
	 /// </p>
	 /// </remarks>
	 static H5TClass getMemberClass(H5DataTypeId^ typeId, 
					unsigned int memberNumber);
	 static String^ getMemberName(H5DataTypeId^ typeId,
				      unsigned int fieldIndex);

	 /// 
	 static void commit(H5LocId^ location, 
			    String^ dataTypeName,
			    H5DataTypeId^ typeId);

	 generic <class Type>
	 static void enumInsert(H5DataTypeId^ typeId,
				String^ name,
				Type% value);
			    
        internal:
	  ///<summary>
	  ///  Get the hid_t from the enumerated standard data type.
	  ///</summary>
	  static hid_t getStdType(H5T::H5Type stdType);

      private:
	 // Disallow the creation of instances of this class.
	 H5T() { };
   };
}
