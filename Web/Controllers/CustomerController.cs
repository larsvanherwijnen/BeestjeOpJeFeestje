using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize(Roles = "Manager")]
public class CustomerController : Controller
{
    private readonly ICrudRepository<Customer> _repository;
    private readonly UserManager<AppUser> _userManager;

    public CustomerController(ICrudRepository<Customer> repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    // GET: Customer
    public IActionResult Index()
    {
        List<Customer> customers = _repository.GetAll();

        List<CustomerViewModel> customerViewModels = customers.Select(customer => new CustomerViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Street = customer.Street,
            HouseNumber = customer.HouseNumber,
            City = customer.City,
            ZipCode = customer.ZipCode,
            Country = customer.Country,
            CustomerCard = customer.CustomerCard
        }).ToList();

        string password = TempData["Password"]?.ToString();
        ViewData["Password"] = password;


        return View(customerViewModels);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        
        Customer customer = _repository.Get(id.Value);
        
        if (customer == null) return NotFound();
        
        CustomerEditViewModel customerEditViewModel = new CustomerEditViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Street = customer.Street,
            HouseNumber = customer.HouseNumber,
            City = customer.City,
            ZipCode = customer.ZipCode,
            Country = customer.Country,
            CustomerCard = customer.CustomerCard
        };
        
        return View(customerEditViewModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CustomerEditViewModel CustomerEditViewModel)
    {
        if (ModelState.IsValid)
        {
            Customer customer = new Customer
            {
                Id = CustomerEditViewModel.Id,
                Name = CustomerEditViewModel.Name,
                Email = CustomerEditViewModel.Email,
                PhoneNumber = CustomerEditViewModel.PhoneNumber,
                Street = CustomerEditViewModel.Street,
                HouseNumber = CustomerEditViewModel.HouseNumber,
                City = CustomerEditViewModel.City,
                ZipCode = CustomerEditViewModel.ZipCode,
                Country = CustomerEditViewModel.Country,
                CustomerCard = CustomerEditViewModel.CustomerCard
            };

            _repository.Update(customer);

            return RedirectToAction(nameof(Index));
        }

        return View(CustomerEditViewModel);
    }

    // GET: Customer/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        
        Customer customer = _repository.Get(id.Value);
        
        if (customer == null) return NotFound();
        
        CustomerViewModel customerViewModel = new CustomerViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Street = customer.Street,
            HouseNumber = customer.HouseNumber,
            City = customer.City,
            ZipCode = customer.ZipCode,
            Country = customer.Country,
            CustomerCard = customer.CustomerCard
        };
        
        return View(customerViewModel);
    }
    
    // GET: Customer/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customer/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CustomerCreateViewModel customer)
    {
        if (ModelState.IsValid)
        {
            Customer newCustomer = new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Street = customer.Street,
                HouseNumber = customer.HouseNumber,
                City = customer.City,
                ZipCode = customer.ZipCode,
                Country = customer.Country,
                CustomerCard = customer.CustomerCard
            };

            AppUser user = new AppUser();
            user.UserName = customer.Email;
            user.Email = customer.Email;
            user.Customer = newCustomer;

            IdentityResult result = _userManager.CreateAsync(user, customer.Password).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Customer").Wait();
            }

            //When calling this the customer is created twice, once by the usermanager and once by the repository
            // _repository.Create(newCustomer);

            TempData["Password"] = customer.Password;
            return RedirectToAction(nameof(Index));
        }


        return View(customer);
    }
}