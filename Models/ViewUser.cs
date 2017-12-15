using System.ComponentModel.DataAnnotations;

namespace DojoActivities.Models
{
    public class ViewUser
    {
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Field must contain only alphabetical characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Field must contain only alphabetical characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string PasswordConf { get; set; }
    }
}