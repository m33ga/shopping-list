using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Models;
using ShoppingList.Domain.Repository;

namespace ShoppingList.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShoppingListDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<User> FindByUserNameAnsPasswordAsync(string userName, string password)
        {
            return await _dbcontext.Users
                .SingleOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _dbcontext.Users
                .SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public override async Task<User> FindOrCreateAsync(User entity)
        {
            var e = await FindByUserNameAsync(entity.UserName);
            if (e == null)
            {
                Create(entity);
                e = entity;
            }

            return e;
        }

        public override async Task<User> UpsertAsync(User entity)
        {
            User c = null;
            User existing = await FindByUserNameAsync(entity.UserName);

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
    }

}
