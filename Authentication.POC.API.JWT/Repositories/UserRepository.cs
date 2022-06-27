using Authentication.POC.API.JWT.Contracts;
using Authentication.POC.API.JWT.Controllers;
using Authentication.POC.API.JWT.Data;
using Authentication.POC.API.JWT.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.POC.API.JWT.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DBContext context) : base(context)
        {
            
        }

        public async Task<User?> Login(string? emailAddress, string? password) =>
            await Task.FromResult(
                Context?.Users?
                        .Include(item => item.Role)
                        .Where(item => item.Email.Equals(emailAddress) && item.Password.Equals(password))
                        .FirstOrDefault());

        public async override Task<IQueryable<User>> GetItems() =>
            await Task.Run(() => 
                Context?.Users?
                        .Include(item => item.Role)
                        .OrderByDescending(item => item.CreatedAt)
                        .AsQueryable()) ?? throw new NullReferenceException();

        public async override Task<User?> GetItem(Guid id) =>
             await Task.Run(() =>
                Context?.Users?
                        .Include(item => item.Role)
                        .Where(item => item.Id.Equals(id))
                        .FirstOrDefault());
    }
}
