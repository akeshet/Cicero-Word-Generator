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

#include "H5Common.h"
#include "HDFException.h"
#include "HDFExceptionSubclasses.h"
#include "H5T.h"

namespace HDF5DotNet
{
   /// <summary>
   /// The H5D contains static member function for each of the supported
   /// H5D calls of the HDF5 library.
   /// </summary>
   public ref class H5D
   {
      public:
	 /// <summary>
	 /// Create a data set at the specified location using a standard
	 /// data type.
	 /// </summary>

         /// <param name="groupOrFileId"> IN: Identifier of the file
         /// or group within which to create the dataset.
         /// </param>

         /// <param name="datasetName"> IN: The name of the dataset to create.
         /// </param>

         /// <param name="dataType"> IN: standard datatype to use when
         /// creating the dataset.
         /// </param>

         /// <param name="dataspaceId"> IN: Identifier of the 
	 /// dataspace to use when creating the dataset.
         /// </param>

	 /// <exception cref="H5DcreateException"> 
	 /// throws H5DcreateException if the data set creation 
	 /// fails 
	 /// </exception>
	 static H5DataSetId^ create(H5LocId^ groupOrFileId, 
				    String^ datasetName,
				    H5T::H5Type dataType,
				    H5DataSpaceId^ dataspaceId);

	 /// <summary>
	 /// Create a data set using a H5DataTypeId.
	 /// </summary>

         /// <param name="groupOrFileId"> IN: Identifier of the file
         /// or group within which to create the dataset.
         /// </param>

         /// <param name="datasetName"> IN: The name of the dataset to create.
         /// </param>

         /// <param name="dataTypeId"> IN: datatypeId to use when
         /// creating the dataset.
         /// </param>

         /// <param name="dataspaceId"> IN: Identifier of the 
	 /// dataspace to use when creating the dataset.
         /// </param>

	 /// <exception cref="H5DcreateException"> 
	 /// throws H5DcreateException if the data set creation 
	 /// fails 
	 /// </exception>
	 static H5DataSetId^ create(H5LocId^ groupOrFileId, 
				    String^ datasetName,
				    H5DataTypeId^ dataTypeId,
				    H5DataSpaceId^ dataspaceId);

	 generic <class Type>
	 static void write(H5DataSetId^ dataSetId,
			   H5DataTypeId^ memTypeId,
			   H5DataSpaceId^ memSpaceId,
			   H5DataSpaceId^ fileSpaceId,
			   H5PropertyListId^ xferPropListId,
			   H5Array<Type>^ data);

	 generic <class Type>
	 static void writeScalar(H5DataSetId^ dataSetId,
			   H5DataTypeId^ memTypeId,
			   H5DataSpaceId^ memSpaceId,
			   H5DataSpaceId^ fileSpaceId,
			   H5PropertyListId^ xferPropListId,
			   Type% data);

	 generic <class Type>
	 static void write(H5DataSetId^ dataSetId,
			   H5DataTypeId^ dataType, 
			   H5Array<Type>^ data);

	 generic <class Type>
	 static void writeScalar(H5DataSetId^ dataSetId,
			   H5DataTypeId^ dataType, 
			   Type% data);

	 generic <class Type>
	 static void read(H5DataSetId^ dataSetId,
			  H5DataTypeId^ memTypeId,
			  H5DataSpaceId^ memSpaceId,
			  H5DataSpaceId^ fileSpaceId,
			  H5PropertyListId^ xferPropListId,
			  H5Array<Type>^ data);

	 generic <class Type>
	 static void readScalar(H5DataSetId^ dataSetId,
			  H5DataTypeId^ memTypeId,
			  H5DataSpaceId^ memSpaceId,
			  H5DataSpaceId^ fileSpaceId,
			  H5PropertyListId^ xferPropListId,
			  Type% data);


	 generic <class Type>
	 static void read(H5DataSetId^ dataSetId, 
			  H5DataTypeId^ dataType, H5Array<Type>^ data);

	 generic <class Type>
	 static void readScalar(H5DataSetId^ dataSetId, 
			  H5DataTypeId^ dataType, 
			  Type% data);

	 /// <summary>
	 /// Close a data set.
	 /// </summary>
	 /// <param name="id">
	 /// IN: Id of data set to close.
	 /// </param>
	 /// <exception cref="H5DcloseException"> 
	 /// throws H5DcloseException if close fails
	 /// </exception>
	 static void close(H5DataSetId^ id);	

	 /// <summary>
	 /// Opens an existing dataset.
	 /// </summary>
	 /// <param name="groupOrFileId">
     /// IN: Identifier of the file or
	 /// group within which the dataset to be accessed will be found. 
	 /// </param>
	 /// <param name="dataSetName">
     /// IN: The name of the dataset to access.
	 /// </param>
	 /// <exception cref="H5DopenException">
	 /// throws H5DopenException on failure.
	 /// </exception>
	 /// <remarks>
     /// H5Dopen opens an existing dataset for access in the file or 
	 /// group specified in groupOrFileId. name is a dataset name and 
	 /// is used to identify the dataset in the file.
	 /// </remarks>
	 static H5DataSetId^ open(H5LocId^ groupOrFileId, String^ dataSetName);

	 
	 ///<summary>
	 /// Returns an identifier for a copy of the dataspace for a
	 ///dataset.
	 ///</summary>
	 ///<param name="dataSetId">
	 /// IN: Identifier of the dataset to query.
	 ///</param>
	 ///<returns>
	 /// Returns a dataspace identifier if successful.
	 ///</returns>
	 ///<remarks>
	 /// H5Dget_space returns an identifier for a copy of the dataspace
	 ///for a dataset. The dataspace identifier should be released with
	 ///the H5S.close function.
	 ///</remarks>
	 ///<exception cref="H5DgetSpaceException">
	 /// throws H5DgetSpaceException on failure.
	 ///</exception>
	 static H5DataSpaceId^ getSpace(H5DataSetId^ dataSetId);


	 ///<summary>
	 /// Returns an identifier for a copy of the datatype for a
	 ///dataset.
	 ///</summary>
	 ///<param name="dataSetId">
	 /// In: Identifier of dataset to query.
	 ///</param>
	 ///<returns>
	 /// Returns a datatype identifier if successful.
	 ///</returns>
	 ///<exception cref="H5DgetTypeException">
	 /// throws H5DgetTypeException on failure.
	 ///</exception>
	 static H5DataTypeId^ getType(H5DataSetId^ dataSetId);
	 

      private:
	 // Disallow instances of this class.
	 H5D() {};
   };
}
