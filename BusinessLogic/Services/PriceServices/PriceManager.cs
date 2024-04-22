using BusinessLogic.Services.PriceServices.DiscountRules;
using BusinessLogic.Services.PriceServices.Interface;
using Data.Models;

namespace BusinessLogic.Services.PriceServices;

public class PriceManager
{
    private readonly List<IDiscountRule> discountRules;

    public PriceManager()
    {
        // Initialize discount rules
        discountRules = new List<IDiscountRule>
        {
            new SameTypeDiscountRule(),
            new EendDiscountRule(),
            new DayOfWeekDiscountRule(),
            new LetterDiscountRule(),
            new CustomerCardDiscountRule(),
        };
    }
    
    public double CalculateDiscount(Booking booking)
    {
        return discountRules.Sum(rule => rule.CalculateDiscount(booking));
    }
    
    public double CalculateTotalPrice(Booking booking)
    {
        double totalPrice = booking.Animals.Sum(animal => animal.Price);
        double discount = CalculateDiscount(booking);
        return totalPrice - discount;
    }
}

