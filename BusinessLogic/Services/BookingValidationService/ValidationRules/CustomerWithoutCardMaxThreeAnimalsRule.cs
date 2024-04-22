using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class CustomerWithoutCardMaxThreeAnimalsRule : IValidationRule
{
    public string Validate(Booking booking )
    {
        if (booking.Customer != null && booking.Customer.CustomerCard == null && booking.Animals.Count > 3)
        {
            return "Customers without a customer card can book max 3 animals";
        }

        return string.Empty;
    }
}