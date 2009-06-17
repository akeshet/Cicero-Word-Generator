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

#include "stdafx.h"
#include "H5Common.h"
#include "H5Fpublic.h" // modified version of H5Fpublic.h
#include "H5Ppublic.h" // original HDF5 library header file
#include "H5Gpublic.h" // modified version of H5Gpublic.h

#include "HDFException.h"
#include "HDFExceptionSubclasses.h"

namespace HDF5DotNet
{
   public enum class H5GType
   {
	GROUP = H5G_GROUP,
	DATASET = H5G_DATASET,
	LINK = H5G_LINK,
	TYPE = H5G_TYPE
   };
   
   public enum class LinkType
   {
        ERROR = H5G_LINK_ERROR,
        HARD = H5G_LINK_HARD,
        SOFT = H5G_LINK_SOFT
   };

   public ref class ObjectInfo 
   {
      internal:
	ObjectInfo(H5G_stat_t% stats);

      public:
	///<summary> file number </summary>
	property array<unsigned long>^ fileNumber 
	{
	   array<unsigned long>^ get() { return fileno_;}
	}

	// object number
	property array<unsigned long>^ objectNumber 
	{
	   array<unsigned long>^ get() { return objno_; }
	}

	// number of hard links to object
	property unsigned int nHardLinks	
	{
	   unsigned int get() { return nlink_; }
	}

	// basic object type
	property H5GType objectType
	{
	   H5GType get() { return type_; }
	}

	// modification time
	property time_t modificationTime
	{
	   time_t get() { return mtime_; }
	}

	// symbolic link value length
	property size_t linkLength
	{
           size_t get() { return linklen_; }
	}

	// H5O_stat_t  members

	// Total size of object header in file
	property hsize_t headerSize
	{
           hsize_t get() { return size_; }
	}

	// Free space within object header
	property hsize_t unusedHeaderSpace
	{
           hsize_t get() { return free_; }
	}

	// Number of object headers messages
	property unsigned int nHeaderMessages
	{
           unsigned int get() { return nmesgs_; }
	}

	// Number of object header chunks
	property unsigned int nHeaderChunks
	{
           unsigned int get() { return nchunks_; }
	}
	       
      private:
	// file number
	array<unsigned long>^  fileno_;

	// object number
	array<unsigned long>^  objno_;

	// number of hard links to object
	unsigned int  nlink_;

	// basic object type
	H5GType  type_;

	// modification time
	time_t  mtime_;

	// symbolic link value length
	size_t  linklen_;

	// H5O_stat_t  members

        // Total size of object header in file
	hsize_t  size_;

	// Free space within object header
	hsize_t  free_;

	// Number of object headers messages
	unsigned int  nmesgs_;

	// Number of object header chunks
	unsigned int  nchunks_;
     };

     public delegate int H5GIterateDelegate(H5GroupId^ group,
                String^ objectName, Object^ parameter);
   /// <summary>
   /// The H5G contains static member function for each of the supported
   /// H5G calls of the HDF5 library.
   /// </summary>

   public ref class H5G
   {
      public:


	    
	 /// <summary>
	 /// Create a HDF5 group.  Creates a new empty group and gives 
	 ///  it a name.
	 /// </summary>
	 /// <param name="groupOrFileId"> IN: provides a group or file id. 
	 /// </param>
	 /// <param name= "groupName"> IN: Absolute or relative name of the
	 /// new group.
	 /// </param>
	 /// <param name="sizeHint">IN: Optional parameter indicating the number
	 /// of bytes to reserve for the names that will appear in the
	 /// group. A conservative estimate could result in multiple
	 /// system-level I/O requests to read the group name heap; a
	 /// liberal estimate could result in a single large I/O request
	 /// even when the group has just a few names. HDF5 stores each
	 /// name with a null terminator. 
	 /// </param>
	 /// <returns> Returns a valid group identifier 
	 /// </returns>
	 /// <remarks>
	 /// <para>
	 /// H5G.create creates a new group with the specified name at the 
	 /// specified location, loc_id. The location is identified by a 
	 /// file or group identifier. The name must not already be 
	 /// taken by some other object and all parent groups must already 
	 /// exist.
	 /// </para>
	 /// <para>
         /// size_hint is a hint for the number of bytes to reserve to 
	 /// store the names which will be eventually added to the new
	 /// group. 
	 /// Passing a value of zero for size_hint is usually adequate
	 /// since the library is able to dynamically resize the name heap,
	 /// but a correct hint may result in better performance. If a 
	 /// non-positive value is supplied for size_hint, then a default  
         /// size is chosen.
	 /// </para>
	 /// <para> 
	 /// The return value is a group identifier for the open group. 
	 /// This group identifier should be closed by calling H5G.close
	 /// when it is no longer needed.  
	 /// </para>
	 /// </remarks>
	 /// <exception>
	 /// throws H5GcreateException if the creation fails.
	 /// </exception>
	 static H5GroupId^ create(H5LocId^ groupOrFileId, String^ groupName,
			size_t sizeHint);


	 /// <summary>
	 ///  Opens an existing group for modification and returns a group
	 /// identifier for that group.
	 /// </summary>
	 /// <param name="groupOrFileId">
	 /// IN: File or group identifier
	 /// within which group is to be open. 
	 /// </param>
	 /// <param name="groupName"> 
	 /// IN: Name of group to open.
	 /// </param>
	 /// <remarks>
	 /// H5G.open opens an existing group with the specified name
	 /// at the specified location, groupOrFileId. The location is 
	 /// identified 
	 /// by a file or group identifier.  H5G.open returns a group
	 /// identifier for the group that was opened. This group
	 /// identifier should be released by calling H5G.close when it is
	 /// no longer needed. 
	 /// </remarks>
	 /// <exception>
	 /// H5G.open throws H5GopenException on failure.
	 /// </exception>
	 static H5GroupId^ open(H5LocId^ groupOrFileId, String^ groupName);
	 
	 /// <summary>
	 /// Closes the specified group.
	 /// </summary>
	 /// <param name="groupId"> IN: Group identifier to release. 
	 /// </param>
	 /// <remarks> H5Gclose releases resources used by a group which
	 /// was opened by H5Gcreate or H5Gopen. After closing a group, the
	 /// group_id cannot be used again. 
	 /// </remarks>
	 /// <exception>
	 /// throws H5GcloseException if the close fails.
	 /// </exception>
	 static void close(H5GroupId^ groupId);

	 /// <summary>
	 /// Gets the number of objects in the specified group.
	 /// </summary>
	 /// <param name="groupId"> IN: Group identifier
	 /// </param>
	 /// <exception cref="H5GgetNumObjectsException"> 
	 /// throws H5GgetNumObjectsException if request fails 
	 /// </exception>
	 /// <remarks> 
	 /// </remarks>
	 static hsize_t getNumObjects(H5GroupId^ groupId);
	 
	 /// <summary>
	 /// Gets the name of the object with the specified object index.
	 /// </summary>
	 /// <param name="groupId"> IN: Group in which object is a
	 /// member.
	 /// </param>
	 /// <exception cref="H5GgetObjectNameByIndexException"> 
	 /// throws H5GgetObjectNameByIndexException if request fails 
	 /// </exception>
	 /// <remarks> 
	 /// </remarks>
	 static String^ getObjectNameByIndex(H5GroupId^ groupId, 
					     int objectIndex);


	 /// <summary>
	 /// Iterates an operation over the entries of a group.
	 /// </summary>
	 /// <param name="loc"> 
	 /// IN: File or group identifier.
	 /// </param>
	 /// <param name="name"> 
	 /// IN: Group over which the iteration is performed.
	 /// </param>
	 /// <param name="func"> 
	 /// IN: Operation to be performed on an object at each step of the
	 /// iteration.
	 /// </param>
	 /// <param name="startIndex"> 
	 ///   IN: Location at which to begin the iteration
	 /// </param>
	 /// <returns>
	 /// Returns the return value of the last operator if it was
	 /// non-zero, or zero if all group members were processed. 
	 /// </returns>
	 /// <remarks> 
	 /// </remarks>
	 static int iterate(H5LocId^ loc, String^ name, 
			    H5GIterateDelegate^ func,
			    Object^ parameters, int startIndex);

	 /// <summary>
	 /// Returns information about an object.
	 /// </summary>
	 /// <param name="groupOrFileId"> 
	 /// IN: File or group Id.
	 /// </param>
	 /// <param name="name"> 
	 /// IN: Name of the object for which status is being sought.
	 /// </param>
	 /// <param name="followLink"> 
	 /// IN: If the object is a symbolic link and followLink is false,
	 /// then the information returned is that for the link itself;
	 /// otherwise the link is followed and information is returned
	 /// about the object to which the link points.
	 /// </param>
	 /// <returns>
	 /// The filenumber and objectnumber properties contain two values
	 /// each.  Together, these four values uniquely
	 /// identify an object among those HDF5 files which are open: if
	 /// all four values are the same between two objects, then the two
	 /// objects are the same (provided both files are still open).
	 /// <ul>
	 /// <li>
	 /// Note that if a file is closed and re-opened, the value in
	 /// fileno will change.
	 /// </li>
	 /// <li>
	 /// If a VFL driver either does not or cannot detect that two
	 /// H5Fopen calls referencing the same file actually open the same
	 /// file, each will get a different fileno.
	 /// </li>
	 /// </ul>
	 /// <p>
	 /// The nHardLinks property is the number of hard links to the
	 /// object or zero when information is being returned about a
	 /// symbolic link (symbolic links do not have hard links but all
	 /// other objects always have at least one).
	 /// </p>
	 /// <p>
	 /// The objectType property contains the type of the object, one of
	 /// H5GType.GROUP, HType.DATASET, HType.LINK, or HType.TYPE.
	 /// </p>
	 /// <p>
	 /// The modificationTime property contains the modification time.
	 /// </p>
	 /// <p>
	 /// If information is being returned about a symbolic link then
	 /// the linkLength property will be the length of the link value 
	 /// (the name of the
	 /// pointed-to object with the null terminator); otherwise it
	 /// will be zero.
	 /// </p>
	 /// <p>
	 /// The headerSize property is the total size of all the object 
	 /// header information in the file (for all chunks).
	 /// </p>
	 /// <p>
	 /// The unusedHeaderSpace property is the size of unused space in
	 /// the object header.
	 /// </p>
	 /// <p>
	 /// The nHeaderMessages property is the number of object header
	 /// messages.
	 /// </p>
	 /// <p>
	 /// The nHeaderChunks property is the number of chunks the 
	 /// object header is broken up into. 
	 /// </p>
	 /// </returns>
	 static ObjectInfo^ getObjectInfo(H5LocId^ groupOrFileId, String^ name,
				   bool followLink);
	    
      private:
	 // There is no reason to create instances of this class.
	 H5G(){};
   };

   // end of H5File implementation

}// end of namespace HDF5DotNet
