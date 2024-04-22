using Data.Models;

namespace Web.ViewModels;

public class BookingViewModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime Date { get; set; }
    public virtual ICollection<AnimalViewModel> Animals { get; set; }
}