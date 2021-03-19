using System.ComponentModel.DataAnnotations;

namespace Auth.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Login is required")]
        public string Login { get; set; }
         
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}