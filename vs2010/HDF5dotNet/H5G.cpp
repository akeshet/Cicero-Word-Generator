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
using namespace System;
using namespace System::Text;
using namespace System::Runtime::InteropServices;

#include "stdafx.h"
#include "H5G.h"

// This prototype is necessary to translate .NET string prototype
// to null-terminated ANSI string, char*, in the HDF library.
// Note: This differs from the current prototype in H5Fpublic.h and
// it will go in H5FpublicNET.h


// ************** MODIFIED BY AVIV KESHET KEYWORD UNDERSCORE_ADD
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
// extern "C" int _cdecl H5Gcreate( // original
#ifdef AVIV_UNDERSCORE_ADD
extern "C" int _cdecl H5G_create( // modified
#else
extern "C" int _cdecl H5Gcreate(
#endif

   int fileId, 
   [MarshalAs(UnmanagedType::LPStr)] 
   String^ groupName, size_t sizeHint);
  

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Gclose(hid_t fileId);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Gopen(hid_t groupOrFileId, 
				 [MarshalAs(UnmanagedType::LPStr)] 
				 String^ groupName);

[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Gget_num_objs(hid_t groupOrFileId, 
					 hsize_t* size);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Gget_objname_by_idx(hid_t group, 
						hsize_t index,
						char* name,
						size_t length);
[DllImport("hdf5dll.dll",
	   CharSet=CharSet::Auto,
	   CallingConvention=CallingConvention::StdCall)]
extern "C" herr_t _cdecl H5Gget_objinfo(hid_t loc, 
					[MarshalAs(UnmanagedType::LPStr)] 
					String^ name,
					hbool_t followLink,
					H5G_stat_t* info);

namespace HDF5DotNet
{
   // Implementation of HDFfile member functions
   H5GroupId^ H5G::create(H5LocId^ groupOrFileId, String^ groupName, 
			  size_t sizeHint)
   {

	   // *************** MODIFIED BY AVIV KESHET KEYWORD UNDERSCORE_ADD

      // Call "C" routine.
      // hid_t id = H5Gcreate(groupOrFileId->Id, groupName, sizeHint); // original line
#ifdef AVIV_UNDERSCORE_ADD
	   hid_t id = H5G_create(groupOrFileId->Id, groupName, sizeHint); // modification
#else 
		hid_t id = H5Gcreate(groupOrFileId->Id, groupName, sizeHint);
#endif

      if (id < 0)
      {
	 String^ message = String::Format(
	    "H5G.create:\n"
	    "  File Id: {0:x} failed to create groupname {1}"
            " with status {2}", groupOrFileId->Id, groupName, id);
	 throw gcnew H5GcreateException(message, id);
      }

      // Success - return a new group id.
      return gcnew H5GroupId(id);
   }

   // Open a group
   H5GroupId^ H5G::open(H5LocId^ groupOrFileId, String^ groupName)
   {
      // Call "C" routine.
      hid_t id = H5Gopen(groupOrFileId->Id, groupName);

      // Check for error status (so we can throw an exception)
      if (id < 0)
      {
	 String^ message = String::Format(
	    "H5G.open:\n"
	    "  File Id: {0:x} failed to open groupname {1} "
            "with status {2}", groupOrFileId->Id, groupName, id);
	 throw gcnew H5GopenException(message, id);
      }

      // Success - return a new group id.
      return gcnew H5GroupId(id);
   }

   // Close a group
   void H5G::close(H5GroupId^ groupId)
   {
      herr_t status  = H5Gclose(groupId->Id);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "Failed to close group Id: {0:x} with status {1}", 
	    groupId->Id, status);
	 throw gcnew H5GcloseException(message, status);
      }
   }

   hsize_t H5G::getNumObjects(H5GroupId^ groupId)
   {
      hsize_t nObjects;
      
      herr_t status  = H5Gget_num_objs(groupId->Id, &nObjects);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5G.getNumberObjects: \n"
	    "  Failed to find number of object: {0:x} with status {1}", 
	    groupId->Id, status);
	 throw gcnew H5GgetNumObjectsException(message, status);
      }

      return nObjects;
   }
   
   String^ H5G::getObjectNameByIndex(H5GroupId^ groupId, int objectIndex)
   {
      // Find the number of characters in the object name.
      size_t nameLength = H5Gget_objname_by_idx(groupId->Id, 
						 objectIndex, 0, 0);

      array<char>^ name;
      
      if (nameLength > 0)
      {
	 // Allocate enough memory to hold the name.
	 name = gcnew array<char>(nameLength+1);
	 
	 // Pin the array in place
	 pin_ptr<char> namePtr = &name[0];
	 
	 // Request the name
	 nameLength = H5Gget_objname_by_idx(groupId->Id, 
					     objectIndex, 
					     namePtr, 
					     nameLength+1);
      }

      // Test again because nameLength can be reset in the previous
      // if-statement.
      if (nameLength <= 0)
      {
	 String^ message = String::Format(
	    "H5G.getObjectNameByIndex: \n"
	    "  Failed to find name of object in group {0:x} with index {1}", 
	    groupId->Id, objectIndex);
	 throw gcnew H5GgetObjectNameByIndexException(message, nameLength);
      }

	  array<wchar_t>^ wideName = gcnew array<wchar_t>(nameLength+1);
	  for(int i=0;i<nameLength;i++) wideName[i] = name[i];

      return gcnew String(wideName,0,nameLength);

   }

   ObjectInfo^ H5G::getObjectInfo(H5LocId^ loc, String^ name,
			   bool followLink)
   {
      // Set the hbool_t data type to true or false depending on the
      // bool value followLink
      hbool_t bfollowLink = (followLink) ? 1 : 0;

      H5G_stat_t info;

      herr_t status = H5Gget_objinfo( loc->Id, name, bfollowLink, &info);

      if (status < 0)
      {
	 String^ message = String::Format(
	    "H5G.getObjectInfo: \n"
	    "  Failed to get info for name \"{0}\" in object {1:x}", 
	    name, loc->Id);
	 throw gcnew H5GgetObjInfoException(message, status);
      }

      return gcnew ObjectInfo(info);
   }
   
   
   int H5G::iterate(H5LocId^ loc, String^ name, 
		    H5GIterateDelegate^ func,
		    Object^ parameters, int startIndex)
   {
      int objectNumber=startIndex;

      // Get the number of objects in the interation.
   
	 // Open specified group
	 H5GroupId^ group = H5G::open(loc, name);
	 
	 hsize_t nObjects = H5G::getNumObjects(group);
	 Console::WriteLine("THere are {0} objects in group {1}",
		 nObjects, name);
	 
	 for(;objectNumber<nObjects;objectNumber++)
	 {
	    // Find the name of the ith object
	    String^ objectName = H5G::getObjectNameByIndex(group, objectNumber);

	    // Invoke the delegate
	    func->Invoke(group, objectName, parameters);
	 }

	 // Close the iteration group.
	 H5G::close(group);
      
   
      // return the index number
      return objectNumber;
   }
  
   /// Copy the information from a H5G_stat_t to an ObjectInfo class.
   ObjectInfo::ObjectInfo(H5G_stat_t% stats):
   nlink_(stats.nlink), type_(safe_cast<H5GType>(stats.type)), mtime_(stats.mtime),
      linklen_(stats.linklen), size_(stats.ohdr.size), free_(stats.ohdr.free),
      nmesgs_(stats.ohdr.nmesgs), nchunks_(stats.ohdr.nchunks)
   {
      fileno_ = 
	 gcnew array<unsigned long>(2) {stats.fileno[0], stats.fileno[1]};

      objno_ = 
	 gcnew array<unsigned long>(2) {stats.objno[0], stats.objno[1]};
   }

}// end of namespace HDF5dotNET
