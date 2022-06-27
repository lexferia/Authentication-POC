using Authentication.POC.API.JWT.Data.Entities;
using Authentication.POC.Web.Shared.DTOs;
using AutoMapper;

namespace Authentication.POC.API.JWT.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(destination => destination.RoleName,
                options => options.MapFrom(source => (source.Role == null ? string.Empty : source.Role.Name)));

            CreateMap<AddUserDTO, User>();
            CreateMap<EditUserDTO, User>();
        }
    }
}
