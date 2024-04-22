using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BookingRepository : ICrudRepository<Booking>
{
    private readonly BeestJeContext _context;
    
    public BookingRepository(BeestJeContext context)
    {
        _context = context;
    }
    
    public bool Create(Booking entity)
    {
        _context.Bookings.Add(entity);
        return _context.SaveChanges() > 0;
    }

    public List<Booking> GetAll()
    {
        return _context.Bookings.Include(Booking => Booking.Animals).Include(Booking => Booking.Customer).ToList();
    }

    public Booking Get(int id)
    {
        return _context.Bookings.Include(Booking => Booking.Animals).Include(Booking => Booking.Customer).FirstOrDefault(Booking => Booking.Id == id);
    }
    
    public Booking Update(Booking entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    // Dirty fix
    public Booking GetByEmailAddress(string customerEmail)
    {
        throw new NotImplementedException();
    }
}