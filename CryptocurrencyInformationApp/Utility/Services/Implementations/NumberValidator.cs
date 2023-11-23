using CryptocurrencyInformationApp.Utility.Services.Abstractions;
using System.Numerics;

namespace CryptocurrencyInformationApp.Utility.Services.Implementations
{
    public class NumberValidator : INumberValidator
    {
        public bool IsInRange<T>(T number, T min, T max) where T : INumber<T>
        {
            if (number >= min && number <= max) 
            {
                return true;
            }
            return false;
        }
    }
}
