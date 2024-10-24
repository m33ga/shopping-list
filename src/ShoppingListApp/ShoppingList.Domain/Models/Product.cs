using System;
using System.Collections.Generic;
using System.Text;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public byte[] Thumb { get; set; } // thumbnail (image of product)
        public int CategoryId { get; set; } // https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx example 4
        public Category Category { get; set; } 
        public override string ToString()
        {
            return Name;
        }
    }
}
