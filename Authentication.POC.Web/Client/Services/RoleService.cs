using Authentication.POC.Web.Client.Contracts;
using Authentication.POC.Web.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Client.Services
{
    internal class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;
        public RoleService(HttpClient httpClient) => _httpClient = httpClient;

        public Task AddItem(AddorEditRoleDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task EditItem(AddorEditRoleDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDTO> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleDTO>> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
