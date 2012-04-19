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
//#include "HDF5ObjLib.h"   // header file of the HDF5 C++/CLI API
#include "H5Common.h"
#include "H5Fpublic.h" // modified version of H5Fpublic.h
#include "H5Ppublic.h"    // original HDF5 library header file
#include "H5Gpublic.h" // modified version of H5Gpublic.h

#include "HDFException.h"
#include "HDFExceptionSubclasses.h"


namespace HDF5DotNet
{
   /// <summary>
   /// The H5F contains static member function for each of the supported
   /// H5F calls of the HDF5 library.  H5F indicates that this group of
   /// function operates on files.
   /// </summary>
   public ref class H5F
   {
      public:
	 /// <summary>
	 /// H5F.CreateMode provides the HDF5 file access modes available
	 /// when creating a file.
	 /// </summary>

	 // sandcastle can't handle the inheritance
	 //enum class CreateMode : unsigned short
	 enum class CreateMode
	 {
	    /// <summary> Read only mode. </summary>
	    ACC_RDONLY = H5F_ACC_RDONLY,	

	    /// <summary> Read/Write mode. </summary>
	    ACC_RDWR = H5F_ACC_RDWR,	

	    /// <summary> Truncate (delete) data in existing file. </summary>
	    ACC_TRUNC = H5F_ACC_TRUNC,

	    /// <summary> Read only mode. </summary>
	    ACC_EXCL = H5F_ACC_EXCL,

	    /// <summary> debug mode. </summary>
	    ACC_DEBUG = H5F_ACC_DEBUG,

	    /// <summary> Create the file. </summary>
	    ACC_CREAT = H5F_ACC_CREAT
	 };

	 /// <summary>
	 /// H5F.OpenMode provides the HDF5 file access modes available
	 /// when opening a file.
	 /// </summary>

	 // sandcastle can't handle the inheritance
	 //enum class OpenMode : unsigned short
	 enum class OpenMode 
	 {
	    /// <summary> Read only mode. </summary>
	    ACC_RDONLY = H5F_ACC_RDONLY,	

	    /// <summary> Read/Write mode. </summary>
	    ACC_RDWR = H5F_ACC_RDWR,	

	    /// <summary> debug mode. </summary>
	    ACC_DEBUG = H5F_ACC_DEBUG
	 };

	 /// <summary> Create a HDF5 file </summary>

	 /// <param name="filename"> filename for new HDF5 file</param>
	 /// <param name="mode"> H5F.CreateMode enumeration type that specifies
	 /// such modes as read-only (H5F.CreateMode.ACC_RDONLY) </param>

         /// <exception> throws H5CreateException when create fails
         /// </exception>

	 /// <returns> a vaild H5FileId for the created file </returns>

	 /// <remarks>
         /// Keeping the H5Id class implemented as an abstract data type 
	 /// provides
         /// us the opportunity for future source-compatible library upgrades.
         /// </remarks>
         /// <example>
         /// <para> [C#] </para>
         /// <code>
         /// try
         /// {
         ///    // Create an HDF5 file.
         ///    H5FileId fileId = H5F.create("myCSharp.h5", 
         ///                                  H5F.CreateMode.ACC_TRUNC);
         ///    H5F.close(openId);
         /// }
         /// catch (HDFException e)
         /// {
         ///    Console.WriteLine(e.Message);
         /// }
         /// </code>
         /// <para> [Visual Basic .NET] </para>
         /// Imports HDF5DotNet
         /// <code>
         /// Module Module1
         ///  Sub Main()
         ///      Dim FileId As H5FileId
         ///         Try
         ///             FileId = H5F.create("HDF5_fromVB", _
	 ///                                  H5F.CreateMode.ACC_TRUNC)
         ///             H5F.close(FileId)
         ///         Catch ex As HDFException
         ///             Console.WriteLine(ex.Message())
         ///         End Try
         ///         Console.WriteLine("Processing complete!")
         ///         Console.ReadLine()
         ///     End Sub
         /// End Module
         /// </code>
         /// <para> [C++/Cli] </para>
	 /// <code>
         /// try
         /// {
         ///   // Create an HDF5 file.
         ///   H5FileId^ fileId = H5F::create("myCpp.h5", 
         ///		                         H5F::CreateMode::ACC_TRUNC);
         ///   // Close the file
         ///   H5F::close(fileId);
         /// }
         /// catch (HDFException^ e)
         /// {
         ///    Console::WriteLine(e->Message);
         /// }
         /// </code>
         /// </example>
	 static H5FileId^ create(String^ filename, 
			    CreateMode mode);

	 /// <summary> Create a HDF5 file </summary>
	 /// <param name="filename"> filename for new HDF5 file</param>
	 /// <param name="mode"> H5F.CreateMode enumeration type that specifies
	 /// such modes as read-only (H5F.CreateMode.ACC_RDONLY) </param>
         /// <exception> throws H5CreateException when create fails
         /// </exception>
	 /// <returns> a vaild H5FileId for the created file </returns>
         /// <remarks>
         /// Keeping the H5Id class implemented as an abstract data type 
	 /// provides
         /// us the opportunity for future source-compatible library upgrades.
         /// </remarks>
	 static H5FileId^ create(String^ filename, 
			    CreateMode mode,
			    H5PropertyListId^ accessPropertyList);

	 /// <summary> Create a HDF5 file </summary>
	 /// <param name="filename"> filename for new HDF5 file</param>
	 /// <param name="mode"> H5F.CreateMode enumeration type that specifies
	 /// such modes as read-only (H5F.CreateMode.ACC_RDONLY) </param>
	 /// <param name="creationPropertyList">
	 /// IN: File creation property list identifier, used when
	 /// modifying default file meta-data.
	 /// </param>
	 /// <param name="accessPropertyList">
	 /// IN: File access property list identifier. If parallel file
	 /// access is desired, this is a collective call according to the
	 /// communicator stored in the access_id.
	 /// </param>
         /// <exception> throws H5CreateException when create fails
         /// </exception>
	 /// <returns> a vaild H5FileId for the created file </returns>
         /// <remarks>
         /// us the opportunity for future source-compatible library upgrades.
         /// </remarks>
	 static H5FileId^ create(String^ filename, 
			    CreateMode mode,
			    H5PropertyListId^ creationPropertyList,
			    H5PropertyListId^ accessPropertyList);

	 /// <summary> open an existing HDF5 file. </summary>
	 ///
	 /// <param name="filename">
	 /// IN: Name of the file to access.
	 /// </param>
	 /// <param name="mode">
	 /// IN: File access mode (e.g., OpenMode.ACC_RDONLY)
	 /// </param>
	 static H5FileId^ open(String^ filename,
			       OpenMode mode);
	 
	 /// <summary> open an existing HDF5 file. </summary>
	 /// <param name="filename">
	 /// IN: Name of the file to access.
	 /// </param>
	 /// <param name="mode">
	 /// IN: File access mode (e.g., OpenMode.ACC_RDONLY)
	 /// </param>
	 /// <param name="propertyListId">
	 /// IN: Identifier for the file access properties list. If
	 /// parallel file access is desired, this is a collective call
	 /// according to the communicator stored in the access_id.
	 /// </param>
	 static H5FileId^ open(String^ filename,
			       OpenMode mode, 
			       H5PropertyListId^ propertyListId);

	 /// <summary> close an open file.  It is necessary to close open 
	 /// files to prevent resource leaks. 
	 /// </summary>
	 static void close(H5FileId^ id);	

	 //enum class Flags : unsigned short
	 //{
	 //   OBJ_FILE = H5F_OBJ_FILE,
	 //   OBJ_DATASET = H5F_OBJ_DATASET,
	 //   OBJ_GROUP = H5F_OBJ_GROUP,
	 //   OBJ_DATATYPE = H5F_OBJ_DATATYPE,
	 //   OBJ_ATTR = H5F_OBJ_ATTR,
	 //   OBJ_ALL = H5F_OBJ_ALL,
	 //   OBJ_LOCAL = H5F_OBJ_LOCAL
	 //};

      private:
	 // Disallow instances of this class.
	 H5F() {};
   };
}
