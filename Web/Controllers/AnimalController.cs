using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize(Roles = "Manager")]
public class AnimalController : Controller
{
    private readonly ICrudRepository<Animal> _animalRepository;

    public AnimalController(ICrudRepository<Animal> animalRepository)
    {
        _animalRepository = animalRepository;
    }

    // GET
    public IActionResult Index()
    {
        var animals = _animalRepository.GetAll();

        var animalViewModel = animals.Select(animal => new AnimalViewModel
        {
            Id = animal.Id,
            Name = animal.Name,
            Type = animal.Type,
            Price = animal.Price,
            Image = animal.Image
        }).ToList();

        return View(animalViewModel);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var animal = _animalRepository.Get(id.Value);

        var animalEditCreateViewModel = new AnimalEditCreateViewModel
        {
            Id = animal.Id,
            Name = animal.Name,
            Type = animal.Type,
            Price = animal.Price,
            Image = animal.Image
        };

        return View(animalEditCreateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AnimalEditCreateViewModel animalEditCreateViewModel)
    {
        if (ModelState.IsValid)
        {
            var animal = new Animal
            {
                Id = animalEditCreateViewModel.Id,
                Name = animalEditCreateViewModel.Name,
                Type = animalEditCreateViewModel.Type,
                Price = animalEditCreateViewModel.Price,
                Image = animalEditCreateViewModel.Image
            };

            _animalRepository.Update(animal);

            return RedirectToAction(nameof(Index));
        }

        return View(animalEditCreateViewModel);
    }

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();

        var animal = _animalRepository.Get(id.Value);
        
        var animalViewModel = new AnimalViewModel
        {
            Id = animal.Id,
            Name = animal.Name,
            Type = animal.Type,
            Price = animal.Price,
            Image = animal.Image,
            Bookings = animal.Bookings
        };

        return View(animalViewModel);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();

        _animalRepository.Delete(id.Value);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        var animalEditCreateViewModel = new AnimalEditCreateViewModel();

        return View(animalEditCreateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AnimalEditCreateViewModel animalEditCreateViewModel)
    {
        if (ModelState.IsValid)
        {
            var animal = new Animal
            {
                Name = animalEditCreateViewModel.Name,
                Type = animalEditCreateViewModel.Type,
                Price = animalEditCreateViewModel.Price,
                Image = animalEditCreateViewModel.Image
            };

            _animalRepository.Create(animal);

            return RedirectToAction(nameof(Index));
        }

        return View(animalEditCreateViewModel);
    }
}