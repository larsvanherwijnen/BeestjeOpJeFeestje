using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class SilverCardAllowsOneExtraAnimalRule : IValidationRule
{
    public string Validate(Booking booking)
    {
        if (booking.Customer != null && booking.Customer.CustomerCard != CustomerCards.Silver && booking.Animals.Count > 4)
        {
            return "Silver card only allows 1 extra animal";
        }

        return String.Empty;
    }
}