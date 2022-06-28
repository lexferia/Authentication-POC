using Authentication.POC.API.JWT.Contracts;
using Authentication.POC.API.JWT.Data;
using Authentication.POC.API.JWT.Data.Entities;

namespace Authentication.POC.API.JWT.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DBContext context) : base(context)
        {
        }
    }
}
