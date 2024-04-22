using Data.Interfaces;
using Data.Models;

namespace BusinessLogic.Services.BookingValidationService.Interface;

public interface IValidationRule
{
    string Validate(Booking booking);
}