using Authentication.POC.Web.Client.Contracts;
using Authentication.POC.Web.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Client.Services
{
    internal class UserService : IUserService
    {
        public Task AddItem(AddUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task EditItem(EditUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
