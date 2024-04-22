using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AnimalRepository : ICrudRepository<Animal>
{
    private readonly BeestJeContext _context;
    
    public AnimalRepository(BeestJeContext context)
    {
        _context = context;
    }
    
    public bool Create(Animal entity)
    {
        _context.Animals.Add(entity);
        return _context.SaveChanges() > 0;
    }

    public List<Animal> GetAll()
    {
        return _context.Animals.ToList();
    }
    
    public Animal Get(int id)
    {
        return _context.Animals.Include(Animal => Animal.Bookings).FirstOrDefault(Animal => Animal.Id == id);
    }

    public Animal Update(Animal entity)
    {
        _context.Animals.Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Delete(int id)
    {
        var animal = Get(id);
        _context.Animals.Remove(animal);
        _context.SaveChanges();
    }

    // Dirty fix
    public Animal GetByEmailAddress(string customerEmail)
    {
        throw new NotImplementedException();
    }
}