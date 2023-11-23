using CryptocurrencyInformationApp.Utility.Services.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CryptocurrencyInformationApp.Utility.Services.Implementations
{
    public class XmlHelper : IXmlHelper
    {
        public void AddElement<T>(T element, string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File with path: {path} not found!");
            }
            List<T> values = new List<T>();
            values = GetElements<T>(path);
            values.Add(element);
            using (StreamWriter sw = new StreamWriter(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(sw, values);
            }
        }

        public List<T> GetElements<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File with path: {path} not found!");
            }
            using (StreamReader sr = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                object output = serializer.Deserialize(sr)!;
                if (output is List<T> res)
                {
                    return res;
                }
                throw new SerializationException("Deserialization was unsuccessfull!");

            }
        }

        public void WriteEmptyCollection<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File with path: {path} not found!");
            }
            List<T> emptyList = new List<T>();
            using (StreamWriter sw = new StreamWriter(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(sw, emptyList);
            }
        }
    }
}
