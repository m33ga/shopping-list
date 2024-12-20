using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using ShoppingList.Domain.Models;
using ShoppingList.Infrastructure;
using ShoppingList.UWP.Converters;

namespace ShoppingList.UWP.ViewModels
{
    public class ShoppingListViewModel
    {
        public ShoppingListEntity ShoppingList { get; set; }
        public Color ShoppingListColor { get; set; }

        public ObservableCollection<ShoppingListEntity> ShoppingLists { get; set; }

        public ShoppingListViewModel()
        {
            ShoppingList = new ShoppingListEntity();
            ShoppingLists = new ObservableCollection<ShoppingListEntity>();
            ShoppingListColor = Color.FromArgb(0, 255, 255, 255);
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var userId = App.UserViewModel.LoggedUser.Id;
                var list = await uow.ShoppingListRepository.FindAllByUserIdAsync(userId);
                ShoppingLists.Clear();
                foreach (var item in list)
                {
                    ShoppingLists.Add(item);
                }
            }

        }

        internal async Task<ShoppingListEntity> UpsertAsync()
        {
            ShoppingListEntity shoppingList = null;
            using (var uow = new UnitOfWork())
            {
                ShoppingList.UserId = App.UserViewModel.LoggedUser.Id;
                if (ShoppingList.Id == 0)
                {
                    ShoppingList.CreationDate = DateTime.Now;
                }

                ShoppingList.Color = ShoppingListColor.ToString();

                shoppingList = await uow.ShoppingListRepository.UpsertAsync(ShoppingList);
                await uow.SaveAsync();
            }

            return shoppingList;
        }

        internal void RefreshItem(ShoppingListEntity model)
        {
            if (model != null)
            {
                ShoppingList = model;
                ShoppingListColor = ColorConverter.GetColorFromCode(model.Color);
            }
        }

        internal async Task<bool> DeleteAsync(ShoppingListEntity model)
        {
            bool res = false;

            using (var uow = new UnitOfWork())
            {
                if (model.ShoppingListProducts.Count == 0)
                {
                    uow.ShoppingListRepository.Delete(model);
                    await uow.SaveAsync();
                    ShoppingLists.Remove(model);
                    res = true;
                }
            }

            return res;
        }

    }
}
