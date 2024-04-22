using Data.Models;

namespace BusinessLogic.Services.PriceServices.Interface;

public interface IDiscountRule
{
    double CalculateDiscount(Booking booking);
}