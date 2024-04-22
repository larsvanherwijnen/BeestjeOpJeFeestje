using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;

namespace Data.Models;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required] public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string HouseNumber { get; set; }
    [Required] public string City { get; set; }
    [Required] public string ZipCode { get; set; }
    [Required] public string Country { get; set; }

    public CustomerCards? CustomerCard { get; set; }
    
    public virtual ICollection<Booking>? Bookings { get; set; }
}