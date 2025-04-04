﻿using ShoppingList.UWP.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingList.UWP.Views.Categories
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryFormPage : Page
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public CategoryFormPage()
        {
            this.InitializeComponent();
            CategoryViewModel = new CategoryViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter  != null)
            {
                CategoryViewModel = e.Parameter as CategoryViewModel;
            }
            base.OnNavigatedTo(e);
        }

        //private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            await CategoryViewModel.UpsertAsync();
            Frame.GoBack();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
