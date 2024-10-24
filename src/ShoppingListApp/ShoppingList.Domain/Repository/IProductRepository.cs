using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Domain.Models;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> FindByNameAsync(string name);
    }
}
