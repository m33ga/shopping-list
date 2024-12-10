using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Domain.Models;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Repository
{
    public interface IUserRepository : IRepository<User> 
    {
        Task<User> FindByUserNameAsync(string userName);
        Task<User> FindByUserNameAnsPasswordAsync(string userName, string password);

    }
}
