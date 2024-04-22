using Data.Models;

namespace Web.ViewModels;

public class BookingBookViewModel
{
    public int? Id { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime Date { get; set; }
    public virtual ICollection<AnimalViewModel>? Animals { get; set; }
    public string? SelectedAnimals { get; set; }

    public double? TotalPrice { get; set; }
    public double? Discount { get; set; }
}