using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Infrastructure.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ShoppingListDbContext _dbcontext;

        public Repository(ShoppingListDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(T entity)
        {
            _dbcontext.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbcontext.Remove(entity);
        }

        public Task<List<T>> FindAllAsync()
        {
            return _dbcontext.Set<T>().ToListAsync();
        }

        public Task<T> FindByIdAsync(int id)
        {
            return _dbcontext.Set<T>().SingleAsync(x => x.Id == id);
        }

        public abstract Task<T> FindOrCreateAsync(T entity);

        public void Update(T entity)
        {
            _dbcontext.Update(entity);
        }

        public abstract Task<T> UpsertAsync(T entity);


    }
}
