using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SpacePotato.Source.World;

namespace SpacePotato {
    public static class ObjectSerializer {
        public static Type[] extraTypes = { typeof(Planet), typeof(Star), typeof(BlackHole) };
        
        public static void Serialize<T>(string absolutePath, T data)
        {
            var serializer = new XmlSerializer(data.GetType(), extraTypes);
            using (var writer = XmlWriter.Create(absolutePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public static T Deserialize<T>(string absolutePath)
        {
            T data;

            var serializer = new XmlSerializer(typeof(T), extraTypes);

            using (var reader = XmlReader.Create(absolutePath))
            {
                var test = (T)serializer.Deserialize(reader);
                data = test;
            }

            return data;
        }
    }

    
    public static class DataSerializer {
        public static void Serialize<T>(string fileName, T data) {
            ObjectSerializer.Serialize(Path.Combine(Paths.dataPath, $"{fileName}.xml"), data);
        }
        
        public static T Deserialize<T>(string filePath) {
            return ObjectSerializer.Deserialize<T>(filePath);
        }
    }
}