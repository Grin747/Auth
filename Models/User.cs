using System;
using Auth.ViewModels;

namespace Auth.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User()
        {
            
        }
        
        public User(RegisterModel model)
        {
            Login = model.Login;
            Password = model.Password;
        }
    }
}