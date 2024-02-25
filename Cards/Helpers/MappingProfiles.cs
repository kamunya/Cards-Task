using AutoMapper;
using Cards.Core.Entities;
using Cards.Dtos;
using CardDto = Cards.Core.Entities.CardDto;

namespace Cards.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Card, CardDto>();
            CreateMap<Address, AddressDto>();
        }
    }
}
