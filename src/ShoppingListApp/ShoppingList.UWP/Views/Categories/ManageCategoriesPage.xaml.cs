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
            throw new NotImplementedException();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
    }
}
