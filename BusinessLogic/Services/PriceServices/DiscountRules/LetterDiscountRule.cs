using BusinessLogic.Services.PriceServices.Interface;
using Data.Models;

namespace BusinessLogic.Services.PriceServices.DiscountRules;

public class LetterDiscountRule : IDiscountRule
{
    public double CalculateDiscount(Booking booking)
    {
        // Rule: 2% extra discount for each occurrence of letters A, B, C, etc. in the animal's name
        string discountLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        double letterDiscount = 0;
        
        foreach (Animal animal in booking.Animals)
        {
            foreach (char discountLetter in discountLetters)
            {
                if (animal.Name.ToUpper().Contains(discountLetter))
                {
                    letterDiscount += 0.02;
                }
                else
                {
                    break;   
                }
            }
        }

        return letterDiscount;
    }
}