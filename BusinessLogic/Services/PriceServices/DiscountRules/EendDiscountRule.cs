using BusinessLogic.Services.PriceServices.Interface;
using Data.Models;

namespace BusinessLogic.Services.PriceServices.DiscountRules;

public class EendDiscountRule : IDiscountRule
{
    public double CalculateDiscount(Booking booking)
    {
        return booking.Animals.Any(a => a.Name == "Eend" && new Random().Next(6) == 0) ? 0.5 : 0;
    }
}