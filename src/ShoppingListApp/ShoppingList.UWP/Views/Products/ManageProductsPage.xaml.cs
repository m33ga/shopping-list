﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ShoppingList.UWP.ViewModels;
using ShoppingList.UWP.Views.Categories;
using ShoppingList.Domain.Models;
using ShoppingList.UWP.Views.ShoppingLists;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingList.UWP.Views.Products
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageProductsPage : Page
    {
        public ProductViewModel ProductViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProductViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        public ManageProductsPage()
        {
            ProductViewModel = new ProductViewModel();
            this.InitializeComponent();
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProductFormPage));
        }

        // file open picker
        private async void BtnUpload_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    byte[] bytes = new byte[(int)stream.Length];
                    await stream.ReadAsync(bytes, 0, (int)stream.Length);
                    ProductViewModel.Product.Thumb = bytes;
                }
                
            }
            else
            {
                // this.textBlock.Text = "Operation cancelled.";
            }
        }


        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Product product)
            {
                ProductViewModel.Product = product;
                Frame.Navigate(typeof(ProductFormPage), ProductViewModel);
            }
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as ShoppingListEntity;
            if (model != null)
            {
                ContentDialog deleteFileDialog = new ContentDialog
                {
                    Title = "Delete Product?",
                    Content = "If you delete this product, you won't be able to recover it. Do you want to delete it?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                };
                ContentDialogResult result = await deleteFileDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    bool res = await new ShoppingListViewModel().DeleteAsync(model);
                    if (!res)
                    {
                        FlyoutBase.ShowAttachedFlyout(GridShoppingList);
                    }
                }
            }
        }
    }
}
