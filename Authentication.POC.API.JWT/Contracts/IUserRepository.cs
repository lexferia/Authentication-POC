using Authentication.POC.API.JWT.Data.Entities;

namespace Authentication.POC.API.JWT.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> Login(string? emailAddress, string? password);
    }
}
