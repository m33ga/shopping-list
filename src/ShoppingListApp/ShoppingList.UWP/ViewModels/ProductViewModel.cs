using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WindowManagement;
using ShoppingList.Domain.Models;
using ShoppingList.Infrastructure;

namespace ShoppingList.UWP.ViewModels
{
    public class ProductViewModel : BindableBase
    {

        public ObservableCollection<ShoppingListProduct> ShoppingListProducts { get; set; }

        public ShoppingListEntity ShoppingList { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value; 
                CategoryName = _product.Category?.Name;
                ProductName = _product?.Name;
                Thumb = _product?.Thumb;
            }

        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set {
                Set(ref _productName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                Set(ref _categoryName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private byte[] _thumb;
        public byte[] Thumb
        {
            get { return _thumb; }
            set { _thumb = value; }
        }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(ProductName) &&
                       !string.IsNullOrEmpty(CategoryName);

            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public ProductViewModel()
        {
            Products = new ObservableCollection<Product>();
            Product = new Product();
            Title = "Products";
            ShoppingListProducts = new ObservableCollection<ShoppingListProduct>();
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ProductRepository.FindAllAsync();
                Products.Clear();
                foreach (var item in list)
                {
                    Products.Add(item);
                }
            }
        }

        internal async Task<ObservableCollection<Category>> LoadCategoriesByNameStartedWithAsync(string categoryName)
        {
            ObservableCollection < Category > res = null;
            using (var uow = new UnitOfWork())
            {
                var list = await uow.CategoryRepository.FindAllByNameStartedWithAsync(categoryName);
                res = new ObservableCollection<Category>(list);
            }

            return res;
        }

        public string Title { get; set; }

        internal async Task<Product> UpsertAsync()
        {
            using (var uow = new UnitOfWork())
            {
                //get categ
                Category category = new Category() { Name = CategoryName };
                Category cateogryUpdated = await uow.CategoryRepository.FindOrCreateAsync(category);
                await uow.SaveAsync();

                //save product
                Product.CategoryId = cateogryUpdated.Id;
                Product.Category = null; //prevent double insert
                Product.Name = ProductName;
                Product.Thumb = Thumb;
                Product prductUpdated = await uow.ProductRepository.UpsertAsync(Product);

                await uow.SaveAsync();

                return prductUpdated;
            }

        }

        internal async void DeleteAsync()
        {
            using (var uow = new UnitOfWork())
            {
                uow.ProductRepository.Delete(Product);
                await uow.SaveAsync();
                Products.Remove(Product);
            }
        }

        public void LoadAllByShoppingList()
        {
            if (ShoppingList.Id != null)
            {
                if (ShoppingList.ShoppingListProducts.Count > 0)
                {
                    ShoppingListProducts.Clear();
                    foreach (var item in ShoppingList.ShoppingListProducts)
                    {
                        ShoppingListProducts.Add(item);
                    }
                }

                Title = $"Products of {ShoppingList.Name}";
            }
            
        }
    }
}
