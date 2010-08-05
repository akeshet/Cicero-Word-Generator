using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Reflection;

namespace DataStructures
{
    /// <summary>
    /// This type converter is to be used in place of ExpandableObjectConverter, when applied to structs. Makes it possible to
    /// edit structs with a propertygrid.
    /// </summary>
    public class ExpandableStructConverter : ExpandableObjectConverter 
    {
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
          
            object ans = Activator.CreateInstance(context.PropertyDescriptor.PropertyType);

            Type objectType = context.PropertyDescriptor.PropertyType;
            foreach (object key in propertyValues.Keys)
            {
            
                    objectType.InvokeMember(key.ToString(), BindingFlags.Public | BindingFlags.NonPublic | 
                                                            BindingFlags.Instance | BindingFlags.SetProperty, 
                                                            null, ans, new Object[] { propertyValues[key] });
            }
            


            return ans;

        }

    }
}
