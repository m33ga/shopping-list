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

namespace ShoppingList.UWP.Views.ShoppingLists
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingListFormPage : Page
    {
        public ShoppingListViewModel ShoppingListViewModel { get; set; }
        public ShoppingListFormPage()
        {
            this.InitializeComponent();
            ShoppingListViewModel = new ShoppingListViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var model = e.Parameter as ShoppingListEntity;
                ShoppingListViewModel.RefreshItem(model);
            }

            base.OnNavigatedTo(e);
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (await ShoppingListViewModel.UpsertAsync() != null)
            {
                Frame.GoBack();
            }
            else
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            }

        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
