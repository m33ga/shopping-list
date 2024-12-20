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
using ShoppingList.UWP.ViewModels;
using ShoppingList.UWP.Views.Categories;
using ShoppingList.UWP.Views.Products;
using ShoppingList.UWP.Views.Users;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShoppingList.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public UserViewModel UserViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
        }

        private void NvMain_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "categories":
                        frmMain.Navigate(typeof(ManageCategoriesPage));
                        break;
                    case "products":
                        frmMain.Navigate(typeof(ManageProductsPage));
                        break;

                }
            }
        }

        private async void NvLogin_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary && UserViewModel.IsLogged)
            {
                frmMain.Navigate(typeof(ManageCategoriesPage));

            }
        }

        private void NvLogout_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            UserViewModel.DoLogout();
            frmMain.BackStack.Clear();
            frmMain.Content = null;

        }



        private async void NvRegister_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            RegisterDialog dlg = new RegisterDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary && UserViewModel.IsLogged)
            {
                frmMain.Navigate(typeof(ManageCategoriesPage));

            }
        }
    }
}
