using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ShoppingList.Domain.Models;
using ShoppingList.UWP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingList.UWP.Views.Products
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsPage : Page
    {
        public ProductViewModel ProductViewModel { get; set; }
        public ProductsPage()
        {
            this.InitializeComponent();
            ProductViewModel = new ProductViewModel();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ProductViewModel.ShoppingList = e.Parameter as ShoppingListEntity;
                ProductViewModel.LoadAllByShoppingList();

            }

            base.OnNavigatedTo(e);
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
