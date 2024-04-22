using System.ComponentModel.DataAnnotations;

namespace Data.Enums;

public enum AnimalTypes
{
    [Display(Name = "Jungle")]
    Jungle,
    [Display(Name = "Boerderij")]
    Farm,
    [Display(Name = "Sneeuw")]
    Snow,
    [Display(Name = "Woestijn")]
    Desert,
    [Display(Name = "Vip")]
    Vip
}