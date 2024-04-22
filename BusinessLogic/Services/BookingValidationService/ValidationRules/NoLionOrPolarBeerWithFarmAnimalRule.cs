using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class NoLionOrPolarBeerWithFarmAnimalRule : IValidationRule
{
    public string Validate(Booking  booking)
    {
        if (booking.Animals.Any(a => a.Name == "Leeuw" || a.Name == "IJsbeer") &&
            booking.Animals.Any(a => a.Type == AnimalTypes.Farm))
        {
            return "Nom nom nom";
        }

        return String.Empty;
    }
}