using System;
using System.Collections.Generic;
using System.Text;
using HDF5DotNet;

namespace Virgil
{
    class VirgilH5Exporter
    {
        public static void testForHDFLibrary()
        {
            H5FileId fileId = H5F.create("hdfLibraryTest", H5F.CreateMode.ACC_TRUNC);
            H5F.close(fileId);
        }
    }
}


// Example code for HDF5 file creation below, pasted from Cyclops

/*

       public static void ExportPictureCollectionToFile(PictureCollection pictures, string fileName)
        {
            H5FileId fileID = H5F.create(fileName, H5F.CreateMode.ACC_TRUNC);
            


            H5GroupId exposureSettingsGroup = H5G.create(fileID, "/Exposure", 0);
            H5GroupId regiongroup = H5G.create(exposureSettingsGroup, "Region", 0);

            H5DataTypeId intDataType = H5T.getNativeType(H5T.H5Type.NATIVE_INT, H5T.Direction.DEFAULT);

            H5DataSpaceId intScalarDataSpace = H5S.create(H5S.H5SClass.SCALAR);


            H5DataSetId x1 = H5D.create(regiongroup, "x1", H5T.H5Type.NATIVE_INT, intScalarDataSpace);
            H5D.writeScalar<int>(x1, intDataType, ref pictures.exposureSettings.exposureRegion.x1);
            H5DataSetId x2 = H5D.create(regiongroup, "x2", intDataType, intScalarDataSpace);
            H5D.writeScalar<int>(x2, intDataType, ref pictures.exposureSettings.exposureRegion.x2);
            H5DataSetId xbin = H5D.create(regiongroup, "xbin", intDataType, intScalarDataSpace);
            H5D.writeScalar<int>(xbin, intDataType, ref pictures.exposureSettings.exposureRegion.xbin);
            H5DataSetId y1 = H5D.create(regiongroup, "y1", intDataType, intScalarDataSpace);
            H5D.writeScalar<int>(y1, intDataType, ref pictures.exposureSettings.exposureRegion.y1);
            H5DataSetId y2 = H5D.create(regiongroup, "y2", intDataType, intScalarDataSpace);
            H5D.writeScalar<int>(y2, intDataType, ref pictures.exposureSettings.exposureRegion.y2);
            H5DataSetId ybin = H5D.create(regiongroup, "ybin", intDataType, intScalarDataSpace);
            H5D.writeScalar<int>(ybin, intDataType, ref pictures.exposureSettings.exposureRegion.ybin);

            H5D.close(x1);
            H5D.close(x2);
            H5D.close(xbin);
            H5D.close(y1);
            H5D.close(y2);
            H5D.close(ybin);



            H5DataSetId xTime = H5D.create(exposureSettingsGroup, "exposureTime_us", intDataType, intScalarDataSpace);
            H5D.writeScalar<int>(xTime, intDataType, ref pictures.exposureSettings.exposureTimeMicroseconds);
            H5D.close(xTime);
            

            H5S.close(intScalarDataSpace);
            H5G.close(regiongroup);
            H5G.close(exposureSettingsGroup);



            H5GroupId imagesGroup = H5G.create(fileID, "Images", 0);

            H5DataTypeId uInt16DataTypeID = H5T.getNativeType(H5T.H5Type.NATIVE_USHORT, H5T.Direction.DEFAULT);

            for (int i = 0; i < pictures.ImageNames.Count; i++)
            {
                string currentName = pictures.ImageNames[i];
                ImageData image = pictures.imageDatas[currentName];

               
                if (image.dataType == typeof(UInt16))
                {
                    UInt16[,] rawDataArray = (UInt16[,])image.getDataMatrix();

                    H5Array<UInt16> h5Array = new H5Array<ushort>(rawDataArray);

                    H5DataSpaceId imageDataSpaceID = H5S.create_simple(2, new ulong[] { (ulong) image.width, (ulong) image.height });

                    H5DataSetId dataSet = H5D.create(imagesGroup, currentName, uInt16DataTypeID, imageDataSpaceID);

                    H5D.write<UInt16>(dataSet, uInt16DataTypeID, h5Array);

                    H5D.close(dataSet);

                    H5S.close(imageDataSpaceID);
                }
            }

            

            //H5T.close(uInt16DataTypeID);

            H5G.close(imagesGroup);
            
            H5F.close(fileID);
        }

        
        public static void TestExportHdf5File(string fileName)
        {
            H5FileId fileID = H5F.create(fileName, H5F.CreateMode.ACC_TRUNC);

            H5DataSpaceId array = H5S.create_simple(1, new ulong [] {10000});

            H5DataTypeId dataTypeID = H5T.getNativeType( H5T.H5Type.NATIVE_USHORT, H5T.Direction.DEFAULT);


            H5DataSetId dataSetID = H5D.create(fileID, "test\\test1", H5T.H5Type.NATIVE_USHORT, array);
            ushort [] data = new ushort[10000];
            for (int i=0; i<10000; i++) {
                data[i] = (ushort)i;
            }
            H5Array<ushort> dataArray = new H5Array<ushort>(data);

            H5D.write(dataSetID, dataTypeID, dataArray);

            H5D.close(dataSetID);
            H5F.close(fileID);
        }
        
    }*/
