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
#include <iostream>
using namespace std;

generic<class Type>
ref class H5Array
{
   public:
      H5Array(array<Type,1>);
      H5Array(array<Type,2>);
      H5Array(array<Type,3>);
      H5Array(array<Type,4>);
      H5Array(array<Type,5>);
      H5Array(array<Type,6>);
      H5Array(array<Type,7>);
      H5Array(array<Type,8>);
      H5Array(array<Type,9>);
      H5Array(array<Type,10>);
      H5Array(array<Type,11>);
      H5Array(array<Type,12>);
      H5Array(array<Type,13>);
      H5Array(array<Type,14>);
      H5Array(array<Type,15>);
      H5Array(array<Type,16>);
      H5Array(array<Type,17>);
      H5Array(array<Type,18>);
      H5Array(array<Type,19>);
      H5Array(array<Type,20>);
      H5Array(array<Type,21>);
      H5Array(array<Type,22>);
      H5Array(array<Type,23>);
      H5Array(array<Type,24>);
      H5Array(array<Type,25>);
      H5Array(array<Type,26>);
      H5Array(array<Type,27>);
      H5Array(array<Type,28>);
      H5Array(array<Type,29>);
      H5Array(array<Type,30>);
      H5Array(array<Type,31>);
      H5Array(array<Type,32>);
      
   private:
      interior_ptr<Type> ptr_;
      System::Array^ array;
};


int main()
{
   const int MAX_RANK = 32;
   int rank;

   for(rank=0;rank<MAX_RANK;rank++)
   {
      cout << "generic <class Type>" << endl;
      cout << "void H5Array::H5Array(
      cout << "array<Type," << rank << "> data)" << endl;
      cout << 
      cout << "   interior_ptr<Type> arrayPtr = &data[0";
      cout << "{" << endl;
      
      for(i=1;i<rank;i++)
      {
	 cout << ",0"
      }
      cout << "];" << endl;
      
      cout << "}" << endl;
   }
   
/*
   for(rank=0;rank<MAX_RANK;rank++)
   {
      
      cout << "generic <class Type>" << endl;
      cout << "void H5D::read(H5DataSetId^ dataSetId," << endl;
      cout << "              H5DataTypeId^ dataType, " << endl;
      cout << "	             array<Type," << rank << "> data)" << endl;
      cout << "{" << endl;
      
      cout << "   interior_ptr<Type> arrayPtr = &data[0";
      for(i=1;i<rank;i++)
      {
	 cout << ",0"
      }
      cout << "];" << endl;
      
      cout << "   read(dataSetId, dataType, memSpaceId, fileSpaceId," << endl;
      cout << "        xferPlistId, arrayPtr);" << endl;
      cout << "}" << endl;
   }
*/
}
