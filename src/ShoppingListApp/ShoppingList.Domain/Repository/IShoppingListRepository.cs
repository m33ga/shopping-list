using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Domain.Models;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Repository
{
    public interface IShoppingListRepository : IRepository<ShoppingListEntity>
    {
        Task<List<ShoppingListEntity>> FindAllByUserIdAsync(int userId);
        Task<ShoppingListEntity> FindByNameAndUserIdAsync(string name, int userId);
    }
}
