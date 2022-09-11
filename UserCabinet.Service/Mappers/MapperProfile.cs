using AutoMapper;
using UserCabinet.Domain.Entities.Users;
using UserCabinet.Service.DTOs.Users;

namespace UserCabinet.Service.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserForCreationDTO, User>().ReverseMap();
        }
    }
}
