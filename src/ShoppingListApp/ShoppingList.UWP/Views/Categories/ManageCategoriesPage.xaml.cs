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

namespace ShoppingList.UWP.Views.Categories
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageCategoriesPage : Page
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public ManageCategoriesPage()
        {
            this.InitializeComponent();
            CategoryViewModel = new CategoryViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CategoryViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoryFormPage));
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Category category)
            {
                CategoryViewModel.Category = category;
                Frame.Navigate(typeof(CategoryFormPage), CategoryViewModel);
            }
        }
        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Category category)
            {
                CategoryViewModel.DeleteAsync(category);
            }
        }
    }
}
