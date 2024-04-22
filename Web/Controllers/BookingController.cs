using System.Dynamic;
using BusinessLogic.Services.BookingValidationService;
using BusinessLogic.Services.PriceServices;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class BookingController : Controller
{
    private readonly ICrudRepository<Booking> _bookingRepository;
    private readonly ICrudRepository<Animal> _animalRepository;
    private readonly ICrudRepository<Customer> _customerRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly BookingValidationManager _bookingValidationManager;
    private readonly PriceManager _priceManager;

    public BookingController(ICrudRepository<Booking> bookingRepository, ICrudRepository<Animal> animalRepository,
        UserManager<AppUser> userManager, BookingValidationManager bookingValidationManager, PriceManager priceManager, ICrudRepository<Customer> customerRepository)
    {
        _bookingRepository = bookingRepository;
        _animalRepository = animalRepository;
        _userManager = userManager;
        _bookingValidationManager = bookingValidationManager;
        _priceManager = priceManager;
        _customerRepository = customerRepository;
    }

    // GET: Booking
    public async Task<IActionResult> Index()
    {
        List<Booking> bookings = _bookingRepository.GetAll();

        AppUser currentUser = await _userManager.GetUserAsync(User);

        if (!User.IsInRole("Manager"))
        {
            bookings = bookings.Where(booking => booking.CustomerId == currentUser.CustomerId).ToList();
        }

        List<BookingViewModel> bookingViewModels = bookings.Select(booking => new BookingViewModel
        {
            Id = booking.Id,
            CustomerId = booking.CustomerId,
            Customer = booking.Customer,
            Date = booking.Date,
            Animals = booking.Animals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
            }).ToList()
        }).ToList();

        return View(bookingViewModels);
    }

    public IActionResult Start()
    {
        // grab the currently logged in user's customer
        BookingBookViewModel bookingBookModel = new BookingBookViewModel();
        return View(bookingBookModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Start(BookingBookViewModel bookingBookViewModel)
    {
        if (ModelState.IsValid)
        {
            var bookingBookModel = new BookingBookViewModel
            {
                Date = bookingBookViewModel.Date,
            };

            return RedirectToAction(nameof(Animals), bookingBookModel);
        }

        return RedirectToAction(nameof(Start));
    }

    [HttpGet]
    public IActionResult Animals(BookingBookViewModel bookingBookViewModel)
    {
        var animals = _animalRepository.GetAll();

        if (ModelState.IsValid)
        {
            var bookingBookModel = new BookingBookViewModel
            {
                Date = bookingBookViewModel.Date,
                Animals = animals.Select(animal => new AnimalViewModel
                {
                    Id = animal.Id,
                    Name = animal.Name,
                    Type = animal.Type,
                    Price = animal.Price,
                    Bookings = animal.Bookings
                }).ToList()
            };

            return View(bookingBookModel);
        }

        return RedirectToAction(nameof(Start));
    }
    
    public IActionResult Contact(BookingBookViewModel bookingBookViewModel)
    {
        var animals = _animalRepository.GetAll();
        var selectedAnimals = bookingBookViewModel.SelectedAnimals.Split(",");
        var bookingAnimals = animals.Where(animal => selectedAnimals.Contains(animal.Id.ToString())).ToList();
        // grab the currently logged in user's customer
        foreach (var animal in bookingAnimals)
        {
            Console.WriteLine(animal.Name);
        }
        Console.WriteLine(bookingBookViewModel.SelectedAnimals);
        var bookingBookModel = new BookingBookViewModel
        {
            Date = bookingBookViewModel.Date,
            Animals = bookingAnimals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                Bookings = animal.Bookings
            }).ToList(),
            SelectedAnimals = bookingBookViewModel.SelectedAnimals
        };
        
        return View(bookingBookModel);
    }

    public IActionResult Finish(BookingBookViewModel bookingBookViewModel)
    {
        var animals = _animalRepository.GetAll();
        var selectedAnimals = bookingBookViewModel.SelectedAnimals.Split(",");
        var bookingAnimals = animals.Where(animal => selectedAnimals.Contains(animal.Id.ToString())).ToList();
        
        var customer = _customerRepository.Get(bookingBookViewModel.CustomerId ?? 0);
        
        var booking = new Booking
        {
            Customer = customer,
            Date = bookingBookViewModel.Date,
            Animals = bookingAnimals,
        }; 
        
        var bookingBookModel = new BookingBookViewModel
        {
            Customer = customer,
            CustomerId = customer.Id,
            Date = bookingBookViewModel.Date,
            Animals = bookingAnimals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                Bookings = animal.Bookings,
            }).ToList(),
            SelectedAnimals = bookingBookViewModel.SelectedAnimals,
            Discount = _priceManager.CalculateDiscount(booking),
            TotalPrice = _priceManager.CalculateTotalPrice(booking)
        };
        return View(bookingBookModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AnimalsPost(BookingBookViewModel bookingBookViewModel)
    {
        var animals = _animalRepository.GetAll();
        var selectedWeeks = bookingBookViewModel.SelectedAnimals.Split(",");

        var BookingAnimals = animals.Where(animal => selectedWeeks.Contains(animal.Id.ToString())).ToList();
        
        var bookingBookModel = new BookingBookViewModel
        {
            Date = bookingBookViewModel.Date,
            SelectedAnimals = bookingBookViewModel.SelectedAnimals,
            Animals = animals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                Bookings = animal.Bookings
            }).ToList()
        };

        Booking booking = new Booking
        {
            Customer = null,
            Date = bookingBookModel.Date,
            Animals = BookingAnimals,
        };

        bool validation = _bookingValidationManager.ValidateBooking(booking, out List<String> errors);

        if (validation)
        {
            ViewBag.Model = errors;
            bookingBookModel.SelectedAnimals = null;
            return View(nameof(Animals), bookingBookModel);
        }
        
        return RedirectToAction(nameof(Contact), bookingBookModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ContactPost(BookingBookViewModel bookingBookViewModel)
    {
        var animals = _animalRepository.GetAll();
        var selectedAnimals = bookingBookViewModel.SelectedAnimals.Split(",");
        var bookingAnimals = animals.Where(animal => selectedAnimals.Contains(animal.Id.ToString())).ToList();
        // grab the currently logged in user's customer
        
        var customer = new Customer
        {
            Name = bookingBookViewModel.Customer.Name,
            Email = bookingBookViewModel.Customer.Email,
            PhoneNumber = bookingBookViewModel.Customer.PhoneNumber,
            Street = bookingBookViewModel.Customer.Street,
            HouseNumber = bookingBookViewModel.Customer.HouseNumber,
            City = bookingBookViewModel.Customer.City,
            ZipCode = bookingBookViewModel.Customer.ZipCode,
            Country = bookingBookViewModel.Customer.Country,
        };
        
        _customerRepository.Create(customer);
        customer = _customerRepository.GetByEmailAddress(customer.Email);
        
        Console.WriteLine(customer.Name);
        
        var bookingBookModel = new BookingBookViewModel
        {
            CustomerId = customer.Id,
            Customer = customer,
            Date = bookingBookViewModel.Date,
            Animals = bookingAnimals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                Bookings = animal.Bookings
            }).ToList(),
            SelectedAnimals = bookingBookViewModel.SelectedAnimals
        };
        
        return RedirectToAction(nameof(Finish), bookingBookModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FinishPost(BookingBookViewModel bookingBookViewModel)
    {
        
        var animals = _animalRepository.GetAll();
        var selectedAnimals = bookingBookViewModel.SelectedAnimals.Split(",");
        var bookingAnimals = animals.Where(animal => selectedAnimals.Contains(animal.Id.ToString())).ToList();
        
        var customer = _customerRepository.Get(bookingBookViewModel.CustomerId ?? 0);
        
        var booking = new Booking
        {
            Customer = customer,
            Date = bookingBookViewModel.Date,
            Animals = bookingAnimals,
        }; 
        
        var bookingBookModel = new BookingBookViewModel
        {
            Customer = customer,
            Date = bookingBookViewModel.Date,
            Animals = bookingAnimals.Select(animal => new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Price = animal.Price,
                Bookings = animal.Bookings,
            }).ToList(),
            SelectedAnimals = bookingBookViewModel.SelectedAnimals,
            Discount = _priceManager.CalculateDiscount(booking),
            TotalPrice = _priceManager.CalculateTotalPrice(booking)
        };
        
        _bookingRepository.Create(booking);
        
        return RedirectToAction(nameof(Index));
    }
}