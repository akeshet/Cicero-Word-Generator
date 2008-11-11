using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{

    /// <summary>
    /// This is a typeconverter class which makes it possible to have our "enum wrapper" classes (like dimension, interpolation type)
    /// to be selectable like enums in a propertygrid. The class must be extended for each different type of enum wrapper, but the extension is
    /// straightforward to do.
    /// </summary>
    public class EnumWrapperTypeConverter : TypeConverter 
    {
        private List<Object> objects;
        public EnumWrapperTypeConverter()
        {
            objects = new List<object>();
        }

        public EnumWrapperTypeConverter(List<object> selectableValues)
        {
            this.objects = selectableValues;
        }

        public EnumWrapperTypeConverter(System.Array selectableValues)
        {
            this.objects = new List<object>();
            foreach (object obj in selectableValues)
                this.objects.Add(obj);
        }

        public EnumWrapperTypeConverter(Object[] selectableValues)
        {
            this.objects = new List<object>(selectableValues);
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(objects);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string str = value as string;
            foreach (Object obj in objects)
                if (obj.ToString() == str)
                    return obj;
            if (objects.Count > 0)
                return objects[0];
            return null;
        }
    }
}
