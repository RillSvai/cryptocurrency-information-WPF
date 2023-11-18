using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CryptocurrencyInformationApp.Utility.Services.Abstractions
{
    public interface IXmlHelper
    {
        public void AddElement<T>(T element, string path);
        public List<T> GetElements<T>(string path);
        public void WriteEmptyCollection<T>(string path);
    }
}
