using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class NoPinguinOnWeekendsRule : IValidationRule
{
    public string Validate(Booking booking)
    { 
        if (booking.Animals.Any(a => a.Name == "Pinguïn") && IsWeekend(booking.Date))
        {
            return "Dieren in pak werken alleen doordeweeks";
        }
        
        return string.Empty;
    }

    private bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }
}