using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace PAKDial.Common
{
    public static class SerializationHelper
    {
        public static string Serialize<T>(T value) where T : class
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(stream, value);
                    stream.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(string SerializedValue)
            where T : class
        {
            if (string.IsNullOrEmpty(SerializedValue))
            {
                return null;
            }

            using (StringReader reader = new StringReader(SerializedValue))
            using (XmlReader xmlReader = XmlReader.Create(reader))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                return serializer.ReadObject(xmlReader) as T;
            }
        }
    }
}
