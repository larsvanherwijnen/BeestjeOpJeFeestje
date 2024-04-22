using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Web.ViewModels;

public class CustomerEditViewModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Ongeldig telefoonnummer. Voer 10 cijfers in.")]
    public string PhoneNumber { get; set; }

    [Required] public string Street { get; set; }
    [Required] public string HouseNumber { get; set; }
    [Required] public string City { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}\s?[A-Za-z]{2}$", ErrorMessage = "Ongeldige postcode. Gebruik het formaat 1234 AB.")]
    public string ZipCode { get; set; }

    [Required] public string Country { get; set; }
    
    public CustomerCards? CustomerCard { get; set; }
}