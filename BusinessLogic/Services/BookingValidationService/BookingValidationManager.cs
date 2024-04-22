using BusinessLogic.Services.BookingValidationService.Interface;
using BusinessLogic.Services.BookingValidationService.ValidationRules;
using Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services.BookingValidationService;

public class BookingValidationManager
{
    private readonly List<IValidationRule> rules;

    public BookingValidationManager()
    {
        // Initialize rules
        rules = new List<IValidationRule>
        {
            new NoLionOrPolarBeerWithFarmAnimalRule(),
            new NoPinguinOnWeekendsRule(),
            new NoDesertInOctoberToFebruaryRule(),
            new NoSnowAnimalsInJuneToAugustRule(),
            new CustomerWithoutCardMaxThreeAnimalsRule(),
            new SilverCardAllowsOneExtraAnimalRule(),
            new NoVipAnimalsForCustomerWithoutAPlatinumCard(),
            // Add your custom rules here
        };
    }

    public bool ValidateBooking(Booking booking, out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        foreach (var rule in rules)
        {
            var result = rule.Validate(booking);
            if (!result.IsNullOrEmpty())
            {
                errorMessages.Add(result);
            }
        }

        return errorMessages.Any();
    }
}