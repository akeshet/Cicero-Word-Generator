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
namespace HDF5DotNet
{
   generic <class Type>
   public ref class H5Array
   {
      public:
         H5Array(array<Type,1>^ theArray);
         H5Array(array<Type,2>^ theArray);
         H5Array(array<Type,3>^ theArray);
         H5Array(array<Type,4>^ theArray);
         H5Array(array<Type,5>^ theArray);
         H5Array(array<Type,6>^ theArray);
         H5Array(array<Type,7>^ theArray);
         H5Array(array<Type,8>^ theArray);
         H5Array(array<Type,9>^ theArray);
         H5Array(array<Type,10>^ theArray);
         H5Array(array<Type,11>^ theArray);
         H5Array(array<Type,12>^ theArray);
         H5Array(array<Type,13>^ theArray);
         H5Array(array<Type,14>^ theArray);
         H5Array(array<Type,15>^ theArray);
         H5Array(array<Type,16>^ theArray);
         H5Array(array<Type,17>^ theArray);
         H5Array(array<Type,18>^ theArray);
         H5Array(array<Type,19>^ theArray);
         H5Array(array<Type,20>^ theArray);
         H5Array(array<Type,21>^ theArray);
         H5Array(array<Type,22>^ theArray);
         H5Array(array<Type,23>^ theArray);
         H5Array(array<Type,24>^ theArray);
         H5Array(array<Type,25>^ theArray);
         H5Array(array<Type,26>^ theArray);
         H5Array(array<Type,27>^ theArray);
         H5Array(array<Type,28>^ theArray);
         H5Array(array<Type,29>^ theArray);
         H5Array(array<Type,30>^ theArray);
         H5Array(array<Type,31>^ theArray);
         H5Array(array<Type,32>^ theArray);

      internal:
         interior_ptr<Type> getDataAddress();

      private:
         System::Array^ arrayHandle_;
   };

   generic<class Type>
   interior_ptr<Type> H5Array<Type>::getDataAddress()
   {
      array<Type,1>^ dim1;
      array<Type,2>^ dim2;
      array<Type,3>^ dim3;
      array<Type,4>^ dim4;
      array<Type,5>^ dim5;
      array<Type,6>^ dim6;
      array<Type,7>^ dim7;
      array<Type,8>^ dim8;
      array<Type,9>^ dim9;
      array<Type,10>^ dim10;
      array<Type,11>^ dim11;
      array<Type,12>^ dim12;
      array<Type,13>^ dim13;
      array<Type,14>^ dim14;
      array<Type,15>^ dim15;
      array<Type,16>^ dim16;
      array<Type,17>^ dim17;
      array<Type,18>^ dim18;
      array<Type,19>^ dim19;
      array<Type,20>^ dim20;
      array<Type,21>^ dim21;
      array<Type,22>^ dim22;
      array<Type,23>^ dim23;
      array<Type,24>^ dim24;
      array<Type,25>^ dim25;
      array<Type,26>^ dim26;
      array<Type,27>^ dim27;
      array<Type,28>^ dim28;
      array<Type,29>^ dim29;
      array<Type,30>^ dim30;
      array<Type,31>^ dim31;
      array<Type,32>^ dim32;

      switch(arrayHandle_->Rank)
      {
         case 1:
           dim1 = reinterpret_cast<array<Type,1>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim1[0]
           );

         case 2:
           dim2 = reinterpret_cast<array<Type,2>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim2[0,0]
           );

         case 3:
           dim3 = reinterpret_cast<array<Type,3>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim3[0,0,0]
           );

         case 4:
           dim4 = reinterpret_cast<array<Type,4>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim4[0,0,0,0]
           );

         case 5:
           dim5 = reinterpret_cast<array<Type,5>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim5[0,0,0,0,0]
           );

         case 6:
           dim6 = reinterpret_cast<array<Type,6>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim6[0,0,0,0,0,0]
           );

         case 7:
           dim7 = reinterpret_cast<array<Type,7>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim7[0,0,0,0,0,0,0]
           );

         case 8:
           dim8 = reinterpret_cast<array<Type,8>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim8[0,0,0,0,0,0,0,0]
           );

         case 9:
           dim9 = reinterpret_cast<array<Type,9>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim9[0,0,0,0,0,0,0,0,0]
           );

         case 10:
           dim10 = reinterpret_cast<array<Type,10>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim10[0,0,0,0,0,0,0,0,0,0]
           );

         case 11:
           dim11 = reinterpret_cast<array<Type,11>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim11[0,0,0,0,0,0,0,0,0,0,0]
           );

         case 12:
           dim12 = reinterpret_cast<array<Type,12>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim12[0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 13:
           dim13 = reinterpret_cast<array<Type,13>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim13[0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 14:
           dim14 = reinterpret_cast<array<Type,14>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim14[0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 15:
           dim15 = reinterpret_cast<array<Type,15>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim15[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 16:
           dim16 = reinterpret_cast<array<Type,16>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim16[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 17:
           dim17 = reinterpret_cast<array<Type,17>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim17[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 18:
           dim18 = reinterpret_cast<array<Type,18>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim18[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 19:
           dim19 = reinterpret_cast<array<Type,19>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim19[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 20:
           dim20 = reinterpret_cast<array<Type,20>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim20[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 21:
           dim21 = reinterpret_cast<array<Type,21>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim21[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 22:
           dim22 = reinterpret_cast<array<Type,22>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim22[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 23:
           dim23 = reinterpret_cast<array<Type,23>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim23[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 24:
           dim24 = reinterpret_cast<array<Type,24>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim24[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 25:
           dim25 = reinterpret_cast<array<Type,25>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim25[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 26:
           dim26 = reinterpret_cast<array<Type,26>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim26[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 27:
           dim27 = reinterpret_cast<array<Type,27>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim27[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 28:
           dim28 = reinterpret_cast<array<Type,28>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim28[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 29:
           dim29 = reinterpret_cast<array<Type,29>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim29[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 30:
           dim30 = reinterpret_cast<array<Type,30>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim30[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 31:
           dim31 = reinterpret_cast<array<Type,31>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim31[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );

         case 32:
           dim32 = reinterpret_cast<array<Type,32>^> (arrayHandle_);
           return interior_ptr<Type>(
           &dim32[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
           );


      } // end switch
   } // end function
   generic<class Type>
   H5Array<Type>::H5Array(array<Type,1>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,2>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,3>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,4>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,5>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,6>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,7>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,8>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,9>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,10>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,11>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,12>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,13>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,14>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,15>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,16>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,17>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,18>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,19>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,20>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,21>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,22>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,23>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,24>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,25>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,26>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,27>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,28>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,29>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,30>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,31>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

   generic<class Type>
   H5Array<Type>::H5Array(array<Type,32>^ arrayHandle) : 
      arrayHandle_(arrayHandle)
   {
   }

} // end namespace
