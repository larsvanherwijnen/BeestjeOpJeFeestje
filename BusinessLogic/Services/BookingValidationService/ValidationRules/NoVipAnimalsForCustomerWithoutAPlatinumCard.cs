using BusinessLogic.Services.BookingValidationService.Interface;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.ValidationRules;

public class NoVipAnimalsForCustomerWithoutAPlatinumCard : IValidationRule
{
    public string Validate(Booking booking)
    {
        if (booking.Customer != null && booking.Customer.CustomerCard != CustomerCards.Platinum && booking.Animals.Any(a => a.Type == AnimalTypes.Vip))
        {
            return "Only customers with a platinum card can book VIP animals";
        }

        return string.Empty;
    }
}