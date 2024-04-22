using Data.Enums;
using Data.Models;

namespace Web.ViewModels;

public class AnimalViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public AnimalTypes Type { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public int? Discount { get; set; }
    
    public ICollection<Booking>? Bookings { get; set; }

}