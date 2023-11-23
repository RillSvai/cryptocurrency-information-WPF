using AutoMapper;
using CryptocurrencyInformationApp.Models;

namespace CryptocurrencyInformationApp.Utility
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Asset,DataGridAsset>();  
        }
    }
}
