using ShoppingList.Domain.Models;
using ShoppingList.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.UWP.ViewModels
{
    public class CategoryViewModel : BindableBase
    {
        public ObservableCollection<Category> Categories { get; set; }
        private Category _category;

        public Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                CategoryName = _category?.Name;
            }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                Set(ref _categoryName, value);
            }
        }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Category = new Category();
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {

                var dbPath = uow.GetDbPath();

                var list = await uow.CategoryRepository.FindAllAsync();
                Categories.Clear();
                foreach (var category in list)
                {
                    Categories.Add(category);
                }
            }
        }

        internal async Task<Category> UpsertAsync()
        {
            Category res = null;

            using (var uow = new UnitOfWork())
            {
                Category.Name = CategoryName;
                res = await uow.CategoryRepository.UpsertAsync(Category);
                await uow.SaveAsync();

            }

            return res;
        }

        internal async void DeleteAsync(Category c)
        {
            using (var uow = new UnitOfWork())
            {
                uow.CategoryRepository.Delete(c);
                await uow.SaveAsync();
                Categories.Remove(c);
            }
        }

    }
}
