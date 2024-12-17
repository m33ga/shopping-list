using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain;
using ShoppingList.Domain.Repository;
using ShoppingList.Infrastructure.Repository;

namespace ShoppingList.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingListDbContext _dbcontext;

        public UnitOfWork()
        {
            _dbcontext = new ShoppingListDbContext();

            // Create db iff it does not exist
            _dbcontext.Database.EnsureCreated();

            // Apply individual migrations
            //_dbcontext.Database.Migrate();
        }

        public IProductRepository ProductRepository 
            => new ProductRepository(_dbcontext);

        public ICategoryRepository CategoryRepository
            => new CategoryRepository(_dbcontext);

        public IUserRepository UserRepository
            => new UserRepository(_dbcontext);

        public IShoppingListRepository ShoppingListRepository
            => new ShoppingListRepository(_dbcontext);

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbcontext.SaveChangesAsync();

        }
        /// <summary>
        /// This method returns the database path
        /// </summary>
        /// <returns></returns>
        public string GetDbPath()
        {
            return _dbcontext.DbPath;
        }
    }
}
