using BusinessLogic.Services.PriceServices.Interface;
using Data.Models;

namespace BusinessLogic.Services.PriceServices.DiscountRules;

public class DayOfWeekDiscountRule : IDiscountRule
{
    public double CalculateDiscount(Booking booking)
    {
        return booking.Date.DayOfWeek == DayOfWeek.Monday || booking.Date.DayOfWeek == DayOfWeek.Tuesday ? 0.15 : 0;
    }
}