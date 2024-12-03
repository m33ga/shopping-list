using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Models;
using ShoppingList.Domain.Repository;

namespace ShoppingList.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository

    {
        public ProductRepository(ShoppingListDbContext dbcontext) : base(dbcontext)
        {
        }

        public Task<Product> FindByNameAsync(string name)
        {
            return _dbcontext.Products
                .SingleOrDefaultAsync(p => p.Name == name);
        }

        public override async Task<Product> FindOrCreateAsync(Product entity)
        {
            var e = await FindByNameAsync(entity.Name);
            if (e == null)
            {
                Create(entity);
                e = entity;
            }

            return e;

        }

        public override Task<Product> UpsertAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Product>> FindAllAsync()
        {
            return _dbcontext.Products
                .Include(x => x.Category)
                .ToListAsync();
        }
    }
}
