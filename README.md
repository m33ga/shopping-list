# Shopping List App  

## Project Overview  
This **UWP (Universal Windows Platform) application** is designed to help users manage their shopping lists efficiently. It includes features for managing shopping lists, products, categories, and users, with authentication and data binding support.  

## Features  
- **NavigationView** with sections for **Shopping Lists, Products, Categories, and Users**, plus **Login, Logout, and Register** options.  
- **Category Management**:  
  - List categories with a **ListView**.  
  - Add, edit, and delete categories.  
  - Use a **Flyout** to display category details.  
- **Product Management**:  
  - List products with a **ListView**.  
  - Add, edit, and delete products.  
  - Use an **AutoSuggestBox** to filter categories while creating products.  
- **User Authentication**:  
  - **Register and Login** using **ContentDialogs**.  
  - Store logged-in user details using **UserViewModel**.  
- **Shopping List Management**:  
  - Display user-specific shopping lists in a **GridView**.  
  - Create shopping lists with a **ColorPicker** for customization.  
  - Edit and delete shopping lists using **MenuFlyout** options.  
- **Shopping List Products**:  
  - View and manage products within a shopping list.  
  - Navigate to the **ProductsPage** from a shopping list.  
  - Add and remove products from a shopping list.  

## Technologies Used  
- **UWP (Universal Windows Platform)**  
- **MVVM Pattern (Model-View-ViewModel)**  
- **Data Binding & NavigationView**  
- **AutoSuggestBox & ColorPicker**  
- **ContentDialogs for Authentication**  
