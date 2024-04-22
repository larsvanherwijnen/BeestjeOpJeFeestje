using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Data;

public class DataSeeder
{
    private readonly BeestJeContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, BeestJeContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public void SeedData()
    {
        SeedRoles();
        SeedUsers();
        SeedAnimals();
        // SeedBookings();
    }
    
    
    private void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Manager").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Manager";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
        
        if (!_roleManager.RoleExistsAsync("Customer").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Customer";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }


    private void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            AppUser adminUser = new AppUser();
            adminUser.UserName = "admin@example.com";
            adminUser.Email = "admin@example.com";
            adminUser.Customer = null;
            
            _context.SaveChanges();
            
            IdentityResult result = _userManager.CreateAsync(adminUser, "Test123!").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(adminUser, "Manager").Wait();
            }

            _context.SaveChanges();
        }
    }

    private void SeedAnimals()
    {
        if (_context.Animals.Any()) return;

        var jungleAnimals = new List<Animal>
        {
            new Animal { Type = AnimalTypes.Jungle, Name = "Aap", Price = 100.00, Image = "aap.jpg" },
            new Animal { Type = AnimalTypes.Jungle, Name = "Olifant", Price = 500.00, Image = "olifant.jpg" },
            new Animal { Type = AnimalTypes.Jungle, Name = "Zebra", Price = 150.00, Image = "zebra.jpg" },
            new Animal { Type = AnimalTypes.Jungle, Name = "Leeuw", Price = 200.00, Image = "leeuw.jpg" }
        };

        var farmAnimals = new List<Animal>
        {
            new Animal { Type = AnimalTypes.Farm, Name = "Hond", Price = 50.00, Image = "hond.jpg" },
            new Animal { Type = AnimalTypes.Farm, Name = "Ezel", Price = 120.00, Image = "ezel.jpg" },
            new Animal { Type = AnimalTypes.Farm, Name = "Koe", Price = 300.00, Image = "koe.jpg" },
            new Animal { Type = AnimalTypes.Farm, Name = "Eend", Price = 30.00, Image = "eend.jpg" },
            new Animal { Type = AnimalTypes.Farm, Name = "Kuiken", Price = 10.00, Image = "kuiken.jpg" }
        };
        
        var snowAnimals = new List<Animal>
        {
            new Animal { Type = AnimalTypes.Snow, Name = "Pinguïn", Price = 80.00, Image = "pinguin.jpg" },
            new Animal { Type = AnimalTypes.Snow, Name = "IJsbeer", Price = 300.00, Image = "ijsbeer.jpg" },
            new Animal { Type = AnimalTypes.Snow, Name = "Zeehond", Price = 120.00, Image = "zeehond.jpg" }
        };

        var desertAnimals = new List<Animal>
        {
            new Animal { Type = AnimalTypes.Desert, Name = "Kameel", Price = 200.00, Image = "kameel.jpg" },
            new Animal { Type = AnimalTypes.Desert, Name = "Slang", Price = 50.00, Image = "slang.jpg" }
        };

        var vipAnimals = new List<Animal>
        {
            new Animal { Type = AnimalTypes.Vip, Name = "T-Rex", Price = 1000.00, Image = "trex.jpg" },
            new Animal { Type = AnimalTypes.Vip, Name = "Unicorn", Price = 500.00, Image = "unicorn.jpg" }
            
        }; 

        _context.Animals.AddRange(jungleAnimals);
        _context.Animals.AddRange(farmAnimals);
        _context.Animals.AddRange(snowAnimals);
        _context.Animals.AddRange(desertAnimals);
        _context.Animals.AddRange(vipAnimals);

        _context.SaveChanges();
    }
}