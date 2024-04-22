using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.Models;

public class Booking
{
    [Key] public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    [Required] public DateTime Date { get; set; }
    public virtual ICollection<Animal> Animals { get; set; }
}