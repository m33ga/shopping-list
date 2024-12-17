using System;
using System.Collections.Generic;
using System.Text;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Models
{
    public class ShoppingListEntity : Entity
    {
        public enum ShoppingListStatus
        {
            Active,
            Achieved
        }
        public string Name { get; set; }
        public string Color { get; set; } 
        public ShoppingListStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public List<ShoppingListProduct> ShoppingListProducts { get; set; }

        public ShoppingListEntity(){}

        public ShoppingListEntity(int id, string name, string color, int userId)
        {
            Id = id;
            Name = name;
            Color = color;
            UserId = userId;
        }
        
    }
}
