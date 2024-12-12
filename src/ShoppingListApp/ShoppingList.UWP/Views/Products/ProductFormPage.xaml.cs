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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingList.UWP.Views.Products
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductFormPage : Page
    {
        public ProductViewModel ProductViewModel { get; set; }

        public ProductFormPage()
        {
            this.InitializeComponent();
            ProductViewModel = new ProductViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ProductViewModel = e.Parameter as ProductViewModel;
            }
            base.OnNavigatedTo(e);
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (await ProductViewModel.UpsertAsync() != null)
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

        private async void AsbCategories_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var list = await ProductViewModel.LoadCategoriesByNameStartedWithAsync(sender.Text);
            sender.ItemsSource = list;

        }

        private void AsbCategories_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                sender.Text = args.ChosenSuggestion.ToString();
            }
        }

        private void AsbCategories_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = args.SelectedItem.ToString();
        }

        private void Btn_Upload_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            // UPLOAD AN IMAGE FROM PC


        }

        
    }
}
