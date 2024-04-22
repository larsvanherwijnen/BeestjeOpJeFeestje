using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Web.ViewModels;

public class AnimalEditCreateViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Naam is verplicht")]
    [Display(Name = "Naam")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Type is verplicht")]
    [Display(Name = "Type")]
    public AnimalTypes Type { get; set; }
    
    [Required(ErrorMessage = "Prijs is verplicht")]
    [Display(Name = "Prijs")]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "Afbeelding is verplicht")]
    [Display(Name = "Afbeelding")]
    public string Image { get; set; }
}