using System;
using System.Collections.Generic;
using System.Text;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
