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

        public override async Task<Product> UpsertAsync(Product entity)
        {
            Product c = null;
            Product existing = await FindByNameAsync(entity.Name);

            if (existing == null)
            {
                if (entity.Id == 0)
                {
                    Create(entity);
                }
                else
                {
                    Update(entity);
                }

                c = entity;
            }
            else if (existing.Id == entity.Id)
            {
                if (existing.Name == entity.Name 
                    || existing.CategoryId != entity.CategoryId 
                    || existing.Thumb != entity.Thumb)
                {
                    _dbcontext.Entry(existing).State = EntityState.Detached;
                    Update(entity);
                }

                c = entity;
            }
            else
            {
                _dbcontext.Entry(entity).State = EntityState.Detached;
            }


            return c;
        }

        public override Task<List<Product>> FindAllAsync()
        {
            return _dbcontext.Products
                .Include(x => x.Category)
                .ToListAsync();
        }
    }
}
