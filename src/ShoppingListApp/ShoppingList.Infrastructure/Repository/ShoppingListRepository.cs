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
    public class ShoppingListRepository : Repository<ShoppingListEntity>, IShoppingListRepository
    {
        public ShoppingListRepository(ShoppingListDbContext dbcontext) : base(dbcontext)
        {
        }

        public override async Task<ShoppingListEntity> FindOrCreateAsync(ShoppingListEntity entity)
        {
            var e = await FindByNameAndUserIdAsync(entity.Name, entity.UserId);
            if (e == null)
            {
                Create(entity);
                e = entity;
            }

            return e;
        }

        public override async Task<ShoppingListEntity> UpsertAsync(ShoppingListEntity entity)
        {
            ShoppingListEntity c = null;
            ShoppingListEntity existing = await FindByNameAndUserIdAsync(entity.Name, entity.UserId);

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
                _dbcontext.Entry(existing).State = EntityState.Detached;
                Update(entity);

                c = entity;
            }
            else
            {
                _dbcontext.Entry(entity).State = EntityState.Detached;
            }


            return c;
        }

        public Task<List<ShoppingListEntity>> FindAllByUserIdAsync(int userId)
        {
            return _dbcontext.ShoppingLists
                .Include(x => x.ShoppingListProducts)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public Task<ShoppingListEntity> FindByNameAndUserIdAsync(string name, int userId)
        {
            return _dbcontext.ShoppingLists
                .SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        }
    }
}
