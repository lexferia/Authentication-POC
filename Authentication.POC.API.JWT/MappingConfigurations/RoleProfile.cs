using Authentication.POC.API.JWT.Data.Entities;
using Authentication.POC.Web.Shared.DTOs;
using AutoMapper;

namespace Authentication.POC.API.JWT.MappingConfigurations
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>();
            CreateMap<AddorEditRoleDTO, Role>();
        }
    }
}
