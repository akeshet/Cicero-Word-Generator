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

#define _H5private_H
#define HDF5_DOT_NET
#include "H5Fpublic.h"
#include "H5T.h"
#include "H5S.h"
#include "H5P.h"



	   // *************** MODIFIED BY AVIV KESHET KEYWORD UNDERSCORE_ADD
//#define AVIV_UNDERSCORE_ADD


// The HDF5DotNet namespace contains all the .NET wrappers for HDF5 library
// calls.
namespace HDF5DotNet
{
   /// <summary>
   ///  H5Id identifies an H5 object.
   /// </summary>
   /// <remarks> 
   /// Only HDF5 library routines may
   /// create an H5Id.  Application programmers use instances of H5Id that
   /// are returned from HDF5 library calls for subsequent calls.
   /// Keeping the H5Id class implemented as an abstract data type provides
   /// the opportunity for future source-compatible library upgrades.
   /// </remarks>
   
   // Store the internal HDF5 id.
   /// <summary>
   /// base class for all classes that provide hid_t to the unmanaged HDF5 API.
   /// </summary>
   /// <remarks>
   ///   H5Id simply stores the hid_t id used by the unmanaged HDF5 API.
   /// Subclasses of this base class provide type saftey.
   /// </remarks>
   public ref class H5Id
   {
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5Id.  It can
	 /// only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5Id(hid_t id);

      internal:
	 /// <summary>
	 /// Allow read-only access to the internal HDF5 id to HDF5 library
	 /// members. (members of this assembly).
	 /// </summary>
	 property hid_t Id
	 {
	    hid_t get() { return id_; }
	 }
	 
      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5Id and they
	 /// must supply an id when doing so.
	 /// </summary>
	 H5Id();
	 
	 /// <summary>
	 // The hdf5 internal id used to identify objects.
	 /// </summary>
	 hid_t id_;
   };

   /// <summary>
   /// H5LocId is the base class for H5FileId and H5GroupId
   /// </summary>
   public ref class H5LocId : public H5Id
   {
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5LocId.  It can
	 /// only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5LocId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5FileId and they
	 /// must supply an id when doing so.
	 /// </summary>
	 H5LocId();
   };
			     

   /// <example>
   /// <para> [C#] </para>
   /// <code>
   /// try
   /// {
   ///    // Create an HDF5 file.
   ///    H5FileId fileId = H5F.create("myCSharp.h5", 
   ///                                  H5F.CreateMode.ACC_TRUNC);
   ///    // Create a HDF5 group.  
   ///    H5GroupId groupId = H5G.create(fileId, "/cSharpGroup", 0);
   ///    H5G.close(groupId);
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
   ///      Dim GroupId As H5GroupId
   /// 
   ///         Try
   ///             FileId = H5F.create("HDF5_fromVB", H5F.CreateMode.ACC_TRUNC)
   ///             GroupId = H5G.create(FileId, "/vbGroup", _
   ///                                 H5F.CreateMode.ACC_TRUNC)
   ///             
   ///             H5G.close(GroupId)
   ///             H5F.close(FileId)
   ///         Catch ex As HDFException
   ///             Console.WriteLine(ex.Message())
   /// 
   ///         End Try
   ///         Console.WriteLine("Processing complete!")
   ///         Console.ReadLine()
   ///     End Sub
   /// End Module
   /// </code>
   /// <para> [C++/Cli] </para>
   /// try
   /// {
   ///   // Create an HDF5 file.
   ///   H5FileId^ fileId = H5F::create("myCpp.h5", 
   ///		                         H5F::CreateMode::ACC_TRUNC);
   ///   // Create a HDF5 group.  
   ///   H5GroupId^ groupId = H5G::create(fileId, "/cppGroup", 0);
   ///
   ///   // Close the group.
   ///   H5G::close(groupId);
   ///
   ///   // Close the file
   ///   H5F::close(fileId);
   /// }
   /// catch (HDFException^ e)
   /// {
   ///    Console::WriteLine(e->Message);
   /// }
   /// <code>
   /// </code>
   /// </example>
   ///
   public ref class H5FileId : public H5LocId
   {
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5FileId.  It can
	 /// only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5FileId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5FileId and they
	 /// must supply an id when doing so.
	 /// </summary>
	 H5FileId();
   };

   
   /// <summary> H5GroupId uniquely identifies a group. </summary>
   public ref class H5GroupId : public H5LocId
   {
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5GroupId.  It can
	 /// only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5GroupId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5GroupId and they
	 /// must supply an id when doing so.
	 /// </summary>
	 H5GroupId();
   };

   /// <summary> H5DataSpaceId uniquely identifies a data space. </summary>
   public ref class H5DataSpaceId : public H5Id
   {
      public:
	 H5DataSpaceId(H5S::H5SType h5Stype);
	 
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5DataSpaceId.
	 /// It can only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5DataSpaceId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5DataSpaceId
	 /// and they must supply an id when doing so.
	 /// </summary>
	 H5DataSpaceId();
   };

   /// <summary> H5DataSetId uniquely identifies a data set. </summary>
   public ref class H5DataSetId : public H5Id
   {
      public:
	 H5DataSetId(H5S::H5SType h5DatasetType);
	 
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5DataSetId.
	 /// It can only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5DataSetId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5DataSetId
	 /// and they must supply an id when doing so.
	 /// </summary>
	 H5DataSetId();
   };

   /// <summary> H5DataTypeId uniquely identifies a data type. </summary>
   public ref class H5DataTypeId : public H5Id
   {
      public:
	 H5DataTypeId(H5T::H5Type h5type);
	 
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an H5DataTypeId.
	 /// It can only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5DataTypeId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of H5DataTypeId
	 /// and they must supply an id when doing so.
	 /// </summary>
	 H5DataTypeId();
   };

   /// <summary>
   /// H5PropertyListId uniquely identifies a property list.
   /// </summary>
   public ref class H5PropertyListId : public H5Id
   {

      public:
	 H5PropertyListId(H5P::Template h5pTemplate);
	 
      internal:
	 /// <remarks>
	 /// This is the only constructor used to create an
	 /// H5PropertyListId.
	 /// It can only be invoked by HDF5DotNet library functions.
	 /// </remarks>
         /// <param name="id"> is an HDF5 library-generated identification
	 /// number that serves as a unique indentifier. </param>
	 H5PropertyListId(hid_t id);

      private:
	 /// <summary>
	 /// The default constructor is disallowed.  Only HDF5 library
	 /// routines are capable of creating an instance of
	 /// H5PropertyListId and they must supply an id when doing so.
	 /// </summary>
	 H5PropertyListId();
   };

}
