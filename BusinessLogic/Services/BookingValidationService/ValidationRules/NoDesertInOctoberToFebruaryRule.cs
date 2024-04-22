using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class NoDesertInOctoberToFebruaryRule : IValidationRule
{
    public string Validate(Booking booking)
    {
        if (booking.Animals.Any(a => a.Type == AnimalTypes.Desert) && IsOctoberToFebruary(booking.Date))
        {
            return "Brrrr – Veelste koud voor een woestijn dier.";
        }
        
        return string.Empty;
    }

    private bool IsOctoberToFebruary(DateTime date)
    {
        return date.Month >= 10 || date.Month <= 2;
    }

}