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

int main()
{
   const int MAX_DIMS = 32;
   int dim;
   
   cout << "namespace HDF5DotNet" << endl;
   cout << "{" << endl;
   
   cout << "   generic <class Type>" << endl;
   cout << "   ref class H5Array" << endl;
   cout << "   {" << endl;
   cout << "      public:" << endl;

   for(dim=1;dim<=MAX_DIMS;dim++)
   {
      cout << "         H5Array(array<Type," << dim << ">^ theArray);" << endl;
   }
   
   cout << endl;
   cout << "      internal:" << endl;
   cout << "         interior_ptr<Type> getDataAddress();" << endl;
   cout << endl;
   cout << "      private:" << endl;
   cout << "         System::Array^ arrayHandle_;" << endl;
   cout << "   };" << endl;
   cout << endl;

   cout << "   generic<class Type>" << endl;
   cout << "   interior_ptr<Type> H5Array<Type>::getDataAddress()" << endl;
   cout << "   {" << endl;

   for(dim=1;dim<=MAX_DIMS;dim++)
   {
      cout << "      array<Type," << dim << ">^ dim" << dim << ";" << endl;
   }

   cout << endl;
   cout << "      switch(arrayHandle_->Rank)" << endl;
   cout << "      {" << endl;
   for(dim=1;dim<=MAX_DIMS;dim++)
   {
      cout << "         case " << dim << ":" << endl;
      cout << "           dim" << dim << " = reinterpret_cast<array<Type,"
	   << dim << ">^> (arrayHandle_);" << endl;
      cout << "           return interior_ptr<Type>(" << endl;
      cout << "           &dim" << dim << "[0";

      for(int i=1;i<dim;i++)
      {
	 cout << ",0";
      }
      cout << "]" << endl;
      cout << "           );" << endl;
      cout << endl;
   }

   cout << endl;
   cout << "      } // end switch" << endl;
   cout << "   } // end function" << endl;

   for(dim=1;dim<=MAX_DIMS;dim++)
   {
      cout << "   generic<class Type>" << endl;
      cout << "   H5Array<Type>::H5Array(array<Type," << dim 
	   << ">^ arrayHandle) : " << endl;
      cout << "      arrayHandle_(arrayHandle)" << endl;
      cout << "   {" << endl;
      cout << "   }" << endl;
      cout << endl;
   }

/*
   cout << "         default:" << endl;
   cout << "            throw H5ArrayException(" << endl;
   cout << "               \"illegal number of dimensions\", " <<  endl;
   cout << "               arrayHandle_->Rank);" << endl;
*/
   cout << "} // end namespace" << endl;
}
