using System.Numerics;

namespace CryptocurrencyInformationApp.Utility.Services.Abstractions
{
    public interface INumberValidator
    {
        public bool IsInRange<T>(T number, T min, T max) where T : INumber<T>;
    }
}
