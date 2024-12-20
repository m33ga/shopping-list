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
using ShoppingList.UWP.Views.Products;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingList.UWP.Views.ShoppingLists
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingListPage : Page
    {
        public ShoppingListViewModel ShoppingListViewModel { get; set; }
        public ShoppingListPage()
        {
            this.InitializeComponent();
            ShoppingListViewModel = new ShoppingListViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.UserViewModel.IsLogged)
            {
                ShoppingListViewModel.LoadAllAsync();
            }
            else
            {
                Frame.Content = null;
            }

            base.OnNavigatedTo(e);
            
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShoppingListFormPage));
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as ShoppingListEntity;
            if (model != null)
            {
                Frame.Navigate(typeof(ProductsPage), model);
            }
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as ShoppingListEntity;
            if (model != null)
            {
                Frame.Navigate(typeof(ShoppingListFormPage), model);
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
