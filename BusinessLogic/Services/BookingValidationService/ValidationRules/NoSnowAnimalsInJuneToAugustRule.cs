using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class NoSnowAnimalsInJuneToAugustRule : IValidationRule
{
    public string Validate(Booking booking)
    {
        if (booking.Animals.Any(a => a.Type == AnimalTypes.Snow) && IsJuneToAugust(booking.Date))
        {
            return "Some People Are Worth Melting For. ~ Olaf";
        }
        
        return String.Empty;
    }

    private bool IsJuneToAugust(DateTime date)
    {
        return date.Month >= 6 && date.Month <= 8;
    }
}