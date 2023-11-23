using CryptocurrencyInformationApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace CryptocurrencyInformationApp.Data
{
    public static class Storage
    {
        private static List<Asset>? _assets;
        private static List<Exchanger>? _exchangers;
        public static List<Asset> Assets { get => _assets!.Select(a => a.Clone() as Asset).ToList()!; set => _assets = value; }
        public static List<Exchanger> Exchangers { get => _exchangers!.Select(e => e.Clone() as Exchanger).ToList()!; set => _exchangers = value;}
    }
}
