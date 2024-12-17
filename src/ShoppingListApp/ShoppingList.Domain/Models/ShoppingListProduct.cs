using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Domain.Models
{
    public class ShoppingListProduct
    {
        public int ShoppingListEntityId { get; set; }
        public ShoppingListEntity ShoppingListEntity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ShoppingListProduct(int shoppingListEntityId, int productId)
        {
            ShoppingListEntityId = shoppingListEntityId;
            ProductId = productId;
        }
    }
}
