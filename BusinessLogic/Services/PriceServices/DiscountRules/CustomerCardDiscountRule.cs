using BusinessLogic.Services.PriceServices.Interface;
using Data.Models;

namespace BusinessLogic.Services.PriceServices.DiscountRules;

public class CustomerCardDiscountRule : IDiscountRule
{
    public double CalculateDiscount(Booking booking)
    {
        return booking.Customer.CustomerCard != null ? 0.1 : 0;
    }
}