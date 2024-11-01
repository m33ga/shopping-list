// See https://aka.ms/new-console-template for more information

//using System.ComponentModel;

using System.Security.Principal;
using ShoppingList.Domain.Models;
using ShoppingList.Infrastructure;

Console.WriteLine("Shopping list testing in console");

var exit = false;

do
{
    Console.Clear();
    Console.WriteLine("App menu");
    Console.WriteLine("--------");
    Console.WriteLine("1. Create categories");
    Console.WriteLine("2. Print categories");
    Console.WriteLine("0. exit");
    var option = Console.ReadLine();
    switch (option)
    {
        case "1":
            CreateCategories();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            break;

        case "2":
            PrintCategories();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            break;

        case "0":
            exit = true;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            break;
        
        default:
            Console.WriteLine("Please enter a valid choice");
            Console.ReadKey();
            break;
    }
} while (!exit);

# region methods
async void PrintCategories()
{
    using (var uow = new UnitOfWork())
    {
        Console.WriteLine($"Database path: {uow.GetDbPath()}");

        var list = await uow.CategoryRepository.FindAllWithDependenciesAsync();
        if (list.Count == 0)
        {
            Console.WriteLine("\n There are no categories yet");
        }
        else
        {
            Console.WriteLine("\n Categories:");
            foreach (var category in list)
            {
                Console.WriteLine("- " + category);
                foreach (var product in category.Products)
                {
                    Console.WriteLine("  - " + product);
                }
            }
        }
    }
}

async void CreateCategories()
{
    using (var uow = new UnitOfWork())
    {
        var p1 = new Product { Name = "Nike Air Force" };
        var p2 = new Product { Name = "Puma Suede" };
        
        var c1 = new Category { Name = "Brands" };
        c1.Products.Add(p1);
        c1.Products.Add(p2);

        uow.CategoryRepository.Create(c1);
        await uow.SaveAsync();

        Console.WriteLine($"\n Created Category: {c1.Name}");
        
    }
}

#endregion


//var c1 = new Category
//{
//    Name = "Drinks"
//};

//var p1 = new Product();
//p1.Name = "Beer";
//var p2 = new Product();
//p2.Name = "Wine";


//c1.Products.Add(p1);
//c1.Products.Add(p2);
//Console.WriteLine($"Category {c1}");
//for (int i = 0; i < c1.Products.Count; i++)
//{
//    Console.WriteLine($"- Prod {i+1} : {c1.Products[i].Name}");
//}

