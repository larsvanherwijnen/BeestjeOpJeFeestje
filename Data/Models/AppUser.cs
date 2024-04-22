using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public class AppUser : IdentityUser
{
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }
}