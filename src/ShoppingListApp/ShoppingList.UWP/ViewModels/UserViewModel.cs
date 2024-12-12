using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Domain.Models;
using ShoppingList.Infrastructure;

namespace ShoppingList.UWP.ViewModels
{
    public class UserViewModel : BindableBase
    {
        public User User { get; set; }

        private User _loggedUser;

        public User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogged));
                OnPropertyChanged(nameof(IsNotLogged));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsLogged
        {
            get => LoggedUser != null;
        }

        public bool IsNotLogged
        {
            get => LoggedUser == null;
        }

        public bool IsAdmin => IsLogged && LoggedUser.IsAdmin;

        private bool _showError;

        public bool ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value; 
                OnPropertyChanged();
            }
        }

        public UserViewModel()
        {
            User = new User();
        }


        internal async Task<bool> DoLoginAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var user = await uow.UserRepository.FindByUserNameAnsPasswordAsync(User.UserName, User.Password);
                
                LoggedUser = user;
                ShowError = user == null;
                
                return !ShowError;
            }
        }

        internal async Task<bool> DoRegisterAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var user = await uow.UserRepository.FindByUserNameAsync(User.UserName);
                if (user != null)
                {
                    ShowError = true;
                
                }

                uow.UserRepository.Create(User);
                await uow.SaveAsync();

                LoggedUser = User;
                ShowError = User == null;
                
            }
            return !ShowError;
        }

        internal void DoLogout()
        {
            LoggedUser = User = new User();
            LoggedUser = null;
        }
    }
}
