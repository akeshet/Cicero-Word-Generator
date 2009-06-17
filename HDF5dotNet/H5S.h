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
#define HDF5_DOT_NET
#include "H5Spublic.h"
#include "HDFException.h"
#include "HDFExceptionSubclasses.h"

namespace HDF5DotNet
{
   ref class H5DataTypeId;
   ref class H5FileId;
   ref class H5LocId;
   ref class H5DataSetId;
   ref class H5DataSpaceId;

   /// <summary>
   /// The H5S contains static member function for each of the supported
   /// H5S calls of the HDF5 library.
   /// </summary>
   public ref class H5S
   {
      public:

	 /// <summary>
	 /// Creates a new simple dataspace and opens it for access.
	 /// </summary>
	 /// <param name="rank"> Number of dimensions of dataspace.
	 /// </param>
	 /// <param name="dims">
	 /// An array of the size of each dimension.
	 /// </param>
	 /// <param name="maxDims">
	 /// An array of the maximum size of each dimension.
	 /// </param>
	 /// <remarks>
	 /// <p>
	 /// H5Screate_simple creates a new simple dataspace and opens it 
	 ///  for access.
	 /// </p>
	 /// <p>
	 /// rank is the number of dimensions used in the dataspace.
	 /// </p>
	 /// <p>
	 /// dims is an array specifying the size of each dimension of the 
	 /// dataset while maxdims is an array specifying the upper limit
	 /// on the size of each dimension. maxdims may be the null
	 /// pointer, in which case the upper limit is the same as dims.
	 /// </p>
	 /// <p>
	 /// If an element of maxdims is H5S_UNLIMITED, (-1), the maximum
	 /// size of the corresponding dimension is unlimited. Otherwise,
	 /// no element of maxdims should be smaller than the corresponding
	 /// element of dims.
	 /// </p>
	 /// <p>
	 /// The dataspace identifier returned from this function must be
	 /// released with H5Sclose or resource leaks will occur. 
	 /// </p>
	 /// </remarks>
	 static H5DataSpaceId^ create_simple(int rank, 
					     array<hsize_t>^ dims,
					     array<hsize_t>^ maxDims);
	 /// <summary>
	 /// Creates a new simple dataspace and opens it for access.
	 /// </summary>
	 /// <param name="rank"> Number of dimensions of dataspace.
	 /// </param>
	 /// <param name="dims">
	 /// An array of the size of each dimension.
	 /// </param>
	 /// <remarks>
	 /// <p>
	 /// H5Screate_simple creates a new simple dataspace and opens it 
	 ///  for access.
	 /// </p>
	 /// <p>
	 /// rank is the number of dimensions used in the dataspace.
	 /// </p>
	 /// <p>
	 /// The upper limit is the same as dims.
	 /// </p>
	 /// <p>
	 /// The dataspace identifier returned from this function must be
	 /// released with H5Sclose or resource leaks will occur. 
	 /// </p>
	 /// </remarks>
	 static H5DataSpaceId^ create_simple(int rank, 
					     array<hsize_t>^ dims);

	 /// <summary>
	 /// Releases and terminates access to a dataspace.
	 /// </summary>
	 /// <param name="id"> Identifier of dataspace to release.
	 /// </param>
	 static void close(H5DataSpaceId^ id);	


	 enum class SelectOperator
	 {
           /// <summary> 
           /// Replaces the existing selection with the 
           /// parameters from this call. Overlapping blocks are not 
           /// supported with this operator. 
           ///</summary>
           SET = H5S_SELECT_SET,

           /// <summary>
           /// Adds the new selection to the existing selection.    
           /// (Binary OR)
           /// </summary>
           OR = H5S_SELECT_OR,

           /// <summary>
           /// Retains only the overlapping portions of the new selection
           /// and the existing selection. (Binary AND)
           /// </summary>
           AND = H5S_SELECT_AND,

           /// <summary>
           /// Retains only the elements that are members of the new 
           /// selection or the existing selection, excluding elements that
           /// are members of both selections. (Binary exclusive-OR, XOR)
           /// </summary>
           XOR = H5S_SELECT_XOR,

           /// <summary>
           /// Retains only elements of the existing selection that are not
           /// in the new selection.
           /// </summary>
           NOTB = H5S_SELECT_NOTB,   	

           /// <summary>
           /// Retains only elements of the new selection that are not in
           /// the existing selection.
           /// </summary>
           NOTA = H5S_SELECT_NOTA 	
	 };
	    
	 enum class H5SType
	 {
            ALL = H5S_ALL,
            UNLIMITED = H5S_UNLIMITED
	 };

	 enum class H5SClass
	 {
	       SCALAR = H5S_SCALAR,
	       SIMPLE = H5S_SIMPLE
	 };
	 
	 /// <summary>
	 /// Creates a new dataspace of a specified type.
	 /// </summary>
	 /// <param name="createClass"> 
	 /// The type of dataspace to be created.
	 /// </param>
	 /// <returns>
	 /// Returns a dataspace identifier if successful.
	 /// </returns>
	 /// <exception cref="H5ScreateException">
	 /// throws H5ScreateException on failure.
	 /// </exception>
	 /// <remarks>
	 /// H5Screate creates a new dataspace of a particular type. The
	 /// types currently supported are H5SClass.SCALAR and H5SClass.SIMPLE;
	 /// others are planned to be added later.
	 /// </remarks>
	 static H5DataSpaceId^ create(H5SClass createClass);
	 
	 /// <summary>
	 /// Selects a hyperslab region to add to the current selected
	 /// region.
	 /// </summary>
	 /// <param name="spaceId">
	 /// IN: Identifier of dataspace selection to modify
	 /// </param>
	 /// <param name="selectOperator">
	 /// IN: Operation to perform on current selection.
	 /// </param>
	 /// <param name="start">
	 /// IN: Offset of start of hyperslab
	 /// </param>
	 /// <param name="stride">
	 /// IN: Hyperslab stride.
	 /// </param> 
	 /// <param name="count">
	 /// IN: Number of blocks included in hyperslab.
	 /// </param> 
	 /// <param name="block"> 
	 /// IN: Size of block in hyperslab.
	 /// </param>
	 /// <remarks>
	 /// <p>
	 /// H5S.selectStridedHyperslab selects a hyperslab region to add to
	 /// the current selected region for the dataspace specified by 
	 /// space_id.
	 /// </p>
	 /// <p>
	 /// The start, stride, count, and block arrays must be the same
	 /// size as the rank of the dataspace.
	 /// </p>
	 /// <p>
	 /// The selection operator op determines how the new selection is
	 /// to be combined with the already existing selection for the 
	 /// dataspace. The following operators are supported:
	 /// </p>
	 /// <ul>
	 /// <li>
	 /// H5S_SELECT_SET - Replaces the existing selection with the 
	 /// parameters from this call. Overlapping blocks are not
	 /// supported with this operator.
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_OR - Adds the new selection to the existing 
         /// selection. (Binary OR)
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_AND - Retains only the overlapping portions of the 
	 /// new selection and the existing selection.    (Binary AND)
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_XOR - Retains only the elements that are members of
	 /// the new selection or the existing selection, excluding
	 /// elements that are members of both selections. (Binary 
	 /// exclusive-OR, XOR)
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_NOTB - Retains only elements of the existing 
	 /// selection that are not in the new selection.
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_NOTA - Retains only elements of the new selection
	 /// that are not in the existing selection.
	 /// </li>
	 /// </ul>
	 /// <p>
	 /// The start array determines the starting coordinates of the 
	 /// hyperslab to select.
	 /// </p>
	 /// <p>
	 /// The stride array chooses array locations from the dataspace
	 /// with each value in the stride array determining how many
	 /// elements to move in each dimension. Setting a value in the 
	 /// stride array to 1 moves to each element in that dimension of
	 /// the dataspace; setting a value of 2 in alocation in the stride
	 /// array moves to every other element in that dimension of the 
	 /// dataspace. In other words, the stride determines the number of
	 /// elements to move from the start location in each dimension. 
	 /// Stride values of 0 are not allowed. 
	 /// </p>
	 /// <p>
	 /// The count array determines how many blocks to select from the 
	 /// dataspace, in each dimension.
	 /// </p>
	 /// <p>
	 /// The block array determines the size of the element block 
	 /// selected from the dataspace. If the block parameter is
	 /// omitted, the block size defaults to a single element in each 
	 /// dimension (as if the block array were set to all 1's).
	 /// </p>
	 /// <p>
 	 /// For example, in a 2-dimensional dataspace, setting start 
	 /// to [1,1], stride to [4,4], count to [3,7], and block to [2,2] 
	 /// selects 21 2x2 blocks of array elements starting with 
	 /// location (1,1) and selecting blocks at locations (1,1), 
	 /// (5,1), (9,1), (1,5), (5,5), etc.
	 /// </p>
	 /// <p>
	 /// Regions selected with this function call default to 
	 /// C order iteration when I/O is performed. 
	 /// </p>
	 /// </remarks>
	 static void selectStridedHyperslab(H5DataSpaceId^ spaceId,
				     H5S::SelectOperator selectOperator,
				     array<hsize_t>^ start,
				     array<hsize_t>^ stride,
				     array<hsize_t>^ count,
				     array<hsize_t>^ block);
	 
				     
	 static void selectStridedHyperslab(H5DataSpaceId^ spaceId,
				     H5S::SelectOperator selectOperator,
				     array<hsize_t>^ start,
				     array<hsize_t>^ stride,
				     array<hsize_t>^ count);
	 
	 /// <summary>
	 /// Selects a hyperslab region to add to the current selected
	 /// region.
	 /// </summary>
	 /// <param name="spaceId">
	 /// IN: Identifier of dataspace selection to modify
	 /// </param>
	 /// <param name="selectOperator">
	 /// IN: Operation to perform on current selection.
	 /// </param>
	 /// <param name="start">
	 /// IN: Offset of start of hyperslab
	 /// </param>
	 /// <param name="count">
	 /// IN: Number of blocks included in hyperslab.
	 /// </param> 
	 /// <param name="block"> 
	 /// IN: Size of block in hyperslab.
	 /// </param>
	 /// <remarks>
	 /// <p>
	 /// H5S.selectStridedHyperslab selects a hyperslab region to add to
	 /// the current selected region for the dataspace specified by 
	 /// space_id.
	 /// </p>
	 /// <p>
	 /// The start, count, and block arrays must be the same
	 /// size as the rank of the dataspace.
	 /// </p>
	 /// <p>
	 /// The selection operator op determines how the new selection is
	 /// to be combined with the already existing selection for the 
	 /// dataspace. The following operators are supported:
	 /// </p>
	 /// <ul>
	 /// <li>
	 /// H5S_SELECT_SET - Replaces the existing selection with the 
	 /// parameters from this call. Overlapping blocks are not
	 /// supported with this operator.
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_OR - Adds the new selection to the existing 
         /// selection. (Binary OR)
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_AND - Retains only the overlapping portions of the 
	 /// new selection and the existing selection.    (Binary AND)
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_XOR - Retains only the elements that are members of
	 /// the new selection or the existing selection, excluding
	 /// elements that are members of both selections. (Binary 
	 /// exclusive-OR, XOR)
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_NOTB - Retains only elements of the existing 
	 /// selection that are not in the new selection.
	 /// </li>
	 /// <li>
	 /// H5S_SELECT_NOTA - Retains only elements of the new selection
	 /// that are not in the existing selection.
	 /// </li>
	 /// </ul>
	 /// <p>
	 /// The start array determines the starting coordinates of the 
	 /// hyperslab to select.
	 /// </p>
	 /// <p>
	 /// The results of calling selectHyperslab are identical to
	 /// calling selectStridedHyperslab, with stride values equal to 1.
	 /// </p>
	 /// <p>
	 /// The count array determines how many blocks to select from the 
	 /// dataspace, in each dimension.
	 /// </p>
	 /// <p>
	 ///     The block array determines the size of the element block 
	 /// selected from the dataspace. If the block parameter is
	 /// omitted, the block size defaults to a single element in each 
	 /// dimension (as if the block array were set to all 1's).
	 /// </p>
	 /// <p>
	 /// Regions selected with this function call default to 
	 /// C order iteration when I/O is performed. 
	 /// </p>
	 /// </remarks>
	 static void selectHyperslab(H5DataSpaceId^ spaceId,
				     H5S::SelectOperator selectOperator,
				     array<hsize_t>^ start,
				     array<hsize_t>^ count,
				     array<hsize_t>^ block);
	 
				     
	 static void selectHyperslab(H5DataSpaceId^ spaceId,
				     H5S::SelectOperator selectOperator,
				     array<hsize_t>^ start,
				     array<hsize_t>^ count);
	 
	 ///<summary>
	 /// Determines the dimensionality of a dataspace.
	 ///</summary>
	 ///<param name="spaceId">
	 /// Identifier of the dataspace
	 ///</param>
	 ///<returns>
	 /// Returns the number of dimensions in the dataspace if
	 ///successful.
	 ///</returns>
	 ///<remarks>
	 /// H5S.getSimpleExtentNDims determines the dimensionality (or
	 ///rank) of a dataspace.
	 ///</remarks>
	 ///<exception cref="H5SgetSimpleExtentNDimsException">
	 /// throws H5SgetSimpleExtentNDimsException on failure.
	 ///</exception>
	 static int getSimpleExtentNDims(H5DataSpaceId^ spaceId);

	 
	 ///<summary>
	 /// H5S.getSimpleExtentMaxDims returns the maximum size of each 
	 /// dimension of a dataspace.
	 ///</summary>
	 ///<param name="spaceId">
	 /// Identifier of the dataspace
	 ///</param>
	 ///<returns>
	 /// An array containing the maximum size of each dimension.
	 ///</returns>
	 ///<remarks>
	 ///</remarks>
	 ///<exception cref="H5SgetSimpleExtentMaxDimsException">
	 /// throws H5SgetSimpleExtentMaxDimsException on failure.
	 ///</exception>
	 static array<hsize_t>^ getSimpleExtentMaxDims(H5DataSpaceId^ spaceId);

	 ///<summary>
	 /// H5S.getSimpleExtentDims returns the size of each 
	 /// dimension of a dataspace.
	 ///</summary>
	 ///<param name="spaceId">
	 /// Identifier of the dataspace
	 ///</param>
	 ///<returns>
	 /// An array containing the size of each dimension.
	 ///</returns>
	 ///<remarks>
	 ///</remarks>
	 ///<exception cref="H5SgetSimpleExtentDimsException">
	 /// throws H5SgetSimpleExtentDimsException on failure.
	 ///</exception>
	 static array<hsize_t>^ getSimpleExtentDims(H5DataSpaceId^ spaceId);

	 ///<summary>
	 /// Verifies that the selection is within the extent of the
	 /// dataspace.
	 ///</summary>
	 ///<param name="spaceId">
	 /// Identifier of the dataspace being queried.
	 ///</param>
	 ///<returns>
	 /// true if the selection is contained within the extent, false if
	 ///it is not.  
	 ///</returns>
	 ///<remarks>
	 ///</remarks>
	 ///<exception cref="H5SselectNoneException">
	 /// throws H5SselectNoneException on failure such as the selection
	 /// or extent not being defined.
	 ///</exception>
	 static bool selectNone(H5DataSpaceId^ spaceId);
	 
	 // Disallow construction of this class.
      private:
	 H5S(){ };
   };

}
