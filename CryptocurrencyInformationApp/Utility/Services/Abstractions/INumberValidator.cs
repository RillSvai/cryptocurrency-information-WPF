using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.Utility.Services.Abstractions
{
    public interface INumberValidator
    {
        public bool IsInRange<T>(T number, T min, T max) where T : INumber<T>;
    }
}
