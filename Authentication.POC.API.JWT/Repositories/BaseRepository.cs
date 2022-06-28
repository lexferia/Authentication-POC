using Authentication.POC.API.JWT.Contracts;
using Authentication.POC.API.JWT.Data;
using Authentication.POC.API.JWT.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Authentication.POC.API.JWT.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public BaseRepository(DBContext context)
        {
            Context = context;
        }

        protected DBContext Context { get; }

        public async Task AddItem(TEntity item)
        {
            if (!Validate(item))
                throw new InvalidDataException();

            item.Id = Guid.NewGuid();
            item.CreatedAt = DateTime.UtcNow; 

            await Context.AddAsync(item);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteItem(Guid id)
        {
            var item = await Context.FindAsync<TEntity>(id);

            if (item is not null)
            {
                Context.Remove(item);
                await Context.SaveChangesAsync();
            }    
        }

        public virtual async Task<TEntity?> GetItem(Guid id) =>
            await Context.FindAsync<TEntity>(id);

        public virtual async Task<IQueryable<TEntity>> GetItems() =>
            await Task.FromResult(Context.Set<TEntity>().AsNoTracking());

        public async Task UpdateItem(TEntity item) 
        {
            if (!Validate(item))
                throw new InvalidDataException();

            item.UpdatedAt = DateTime.UtcNow;

            Context.Update(item);

            await Context.SaveChangesAsync();
        }

        private bool Validate(TEntity entity) =>
            Validator.TryValidateObject(entity, new ValidationContext(entity), null);
    }
}
