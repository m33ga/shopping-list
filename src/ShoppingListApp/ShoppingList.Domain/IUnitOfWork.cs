using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Domain.Repository;

namespace ShoppingList.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        // deal with database operations -> IDisposable
        // bridge between repos and rest of code
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task SaveAsync();
    }
}
