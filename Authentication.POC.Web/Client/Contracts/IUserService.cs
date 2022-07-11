using Authentication.POC.Web.Shared.DTOs;

namespace Authentication.POC.Web.Client.Contracts
{
    public interface IUserService : IService<UserDTO, AddUserDTO, EditUserDTO>
    {
    }
}
