using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ShoppingList.Domain.SeedWork;

namespace ShoppingList.Domain.Models
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string _password { get; set; }

        public string Password
        {
            get { return _password; }
            set
            {
                var data = Encoding.UTF8.GetBytes(value);
                var hashData = new SHA1Managed().ComputeHash(data);
                _password = BitConverter.ToString(hashData).Replace("-", "").ToUpper();
            }
        }

        public bool IsAdmin { get; set; }
    
    }
}
