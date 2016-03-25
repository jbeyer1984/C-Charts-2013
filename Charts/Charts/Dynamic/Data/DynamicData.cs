using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Reflection;

namespace Charts
{
    [Serializable()]
    [AttributeUsage(AttributeTargets.Property)]
    abstract public class DynamicData : Attribute
    {
        //private PropertyInfo _property;

        // The Property of the targeted class
        // that’s being validated
        //public PropertyInfo Property
        //{
        //    get { return _property; }
        //    set { _property = value; }
        //}

        //public string PropertyName
        //{
        //    get
        //    {
        //        return _property.Name;
        //    }
        //}
    }

    public static class ExtensionMethodsClass
    {
        public static string ToJSON<T>(this T obj) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }
    }
}
