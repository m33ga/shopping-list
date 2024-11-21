using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Models;
using ShoppingList.Domain.Repository;

namespace ShoppingList.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShoppingListDbContext dbcontext) : base(dbcontext)
        {
        }

        public Task<List<Category>> FindAllByNameStartedWithAsync(string text)
        {
            return _dbcontext.Categories
                .Where(c => c.Name.StartsWith(text))
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public Task<List<Category>> FindAllWithDependenciesAsync()
        {
            return _dbcontext.Categories
                .Include(x => x.Products)
                .ToListAsync();
        }

        public Task<Category> FindByNameAsync(string name)
        {
            return _dbcontext.Categories
                .SingleOrDefaultAsync(c =>  c.Name == name);                
                
        }

        public override async Task<Category> FindOrCreateAsync(Category entity)
        {
            var e = await FindByNameAsync(entity.Name);
            if (e == null)
            {
                Create(entity);
                e = entity;
            }
            return e;
        }

        public override async Task<Category> UpsertAsync(Category entity)
        {
            Category c = null;
            Category existing = await FindByNameAsync(entity.Name);

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
                if (existing.Name == entity.Name)
                    Update(entity);

                c = entity;
            }
            else
            {
                _dbcontext.Entry(entity).State = EntityState.Detached;
            }


            return c;

        }
    }
}
