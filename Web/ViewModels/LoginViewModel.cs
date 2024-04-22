using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class LoginViewModel
{
    
    [Required(ErrorMessage = "Email is verplicht")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Wachtwoord is verplicht")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord")]
    public string Password { get; set; } 
    
}