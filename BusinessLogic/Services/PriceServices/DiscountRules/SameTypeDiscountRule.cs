using BusinessLogic.Services.PriceServices.Interface;
using Data.Models;

namespace BusinessLogic.Services.PriceServices.DiscountRules;

public class SameTypeDiscountRule : IDiscountRule
{
    public double CalculateDiscount(Booking booking)
    {
        return booking.Animals.Count(a => a.Type == booking.Animals.First().Type) >= 3 ? 0.1 : 0;
    }
}