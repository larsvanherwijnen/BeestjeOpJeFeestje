using Data.Enums;
using Data.Models;

namespace Web.ViewModels;

public class CustomerViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }

    public CustomerCards? CustomerCard { get; set; }
    
    public ICollection<Booking>? Bookings { get; set; }
}