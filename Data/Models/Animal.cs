using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.Models;

public class Animal
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public AnimalTypes Type { get; set; }
    [Required] public double Price { get; set; }
    [Required] public string Image { get; set; }
    
    public virtual ICollection<Booking> Bookings { get; set; }

    
}