using System.Collections.Generic;

namespace CryptocurrencyInformationApp.Utility.Services.Abstractions
{
    public interface IXmlHelper
    {
        public void AddElement<T>(T element, string path);
        public List<T> GetElements<T>(string path);
        public void WriteEmptyCollection<T>(string path);
    }
}
