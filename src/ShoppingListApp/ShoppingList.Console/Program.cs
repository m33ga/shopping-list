// See https://aka.ms/new-console-template for more information

//using System.ComponentModel;
using ShoppingList.Domain.Models;

Console.WriteLine("Shopping list testing in console");

var c1 = new Category
{
    Name = "Drinks"
};

var p1 = new Product();
p1.Name = "Beer";
var p2 = new Product();
p2.Name = "Wine";


c1.Products.Add(p1);
c1.Products.Add(p2);
Console.WriteLine($"Category {c1}");
for (int i = 0; i < c1.Products.Count; i++)
{
    Console.WriteLine($"- Prod {i+1} : {c1.Products[i].Name}");
}