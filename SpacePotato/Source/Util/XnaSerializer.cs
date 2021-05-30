using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SpacePotato {
    public static class ObjectSerializer
    {
        public static void Serialize<T>(string absolutePath, T data)
        {
            var serializer = new XmlSerializer(data.GetType());
            using (var writer = XmlWriter.Create(absolutePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public static T Deserialize<T>(string absolutePath)
        {
            T data;

            var serializer = new XmlSerializer(typeof(T));

            using (var reader = XmlReader.Create(absolutePath))
            {
                var test = (T)serializer.Deserialize(reader);
                data = test;
            }

            return data;
        }
    }

    
    public static class DataSerializer {
        public static void Serialize<T>(string filePath, T data) {
            ObjectSerializer.Serialize(filePath, data);
        }
        
        public static T Deserialize<T>(string filePath) {
            return ObjectSerializer.Deserialize<T>(filePath);
        }
    }
}