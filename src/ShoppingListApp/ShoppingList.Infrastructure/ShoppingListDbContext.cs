using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ShoppingList.Domain.Models;

namespace ShoppingList.Infrastructure
{
    public class ShoppingListDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingListEntity> ShoppingLists { get; set; }
        public DbSet<ShoppingListProduct> ShoppingListProducts { get; set; }

        public string DbPath { get; }

        public ShoppingListDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Combine(path, "2024ShoppingListClassA-withUsers.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        // fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);


            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);


            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<User>()
                .Property(x => x.IsAdmin)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        UserName = "admin",
                        Password = "admin",
                        IsAdmin = true
                    }
                );


            modelBuilder.Entity<ShoppingListEntity>()
                .HasIndex(x => new {x.Name, x.UserId})
                .IsUnique();
            modelBuilder.Entity<ShoppingListEntity>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<ShoppingListEntity>()
                .Property(x => x.Color)
                .HasMaxLength(9);
            modelBuilder.Entity<ShoppingListEntity>()
                .Property(x => x.Status)
                .IsRequired();
            modelBuilder.Entity<ShoppingListEntity>()
                .Property(x => x.CreationDate)
                .IsRequired();
            modelBuilder.Entity<ShoppingListEntity>().HasData(
                new ShoppingListEntity(1, "list 1", "#FF0000FF", 1),
                new ShoppingListEntity(2, "list 2", "#FF0000FF", 1)
            );


            modelBuilder.Entity<ShoppingListProduct>()
                .HasIndex(x => new { x.ShoppingListEntityId, x.ProductId })
                .IsUnique();
            modelBuilder.Entity<ShoppingListProduct>().HasData(
                new ShoppingListProduct(1, 1),
                new ShoppingListProduct(2, 1),
                new ShoppingListProduct(2, 2)
            );




        }

    }
}
