using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository : ICrudRepository<Customer>
{
    private readonly BeestJeContext _context;

    public CustomerRepository(BeestJeContext context)
    {
        _context = context;
    }

    public bool Create(Customer entity)
    {
        _context.Customers.Add(entity);
        return _context.SaveChanges() > 0;
    }

    public List<Customer> GetAll()
    {
        return _context.Customers.ToList();
    }

    public Customer Get(int id)
    {
        return _context.Customers.Include(Customer => Customer.Bookings).FirstOrDefault(Customer => Customer.Id == id);
    }

    public Customer GetByEmailAddress(string emailAddress)
    {
        return _context.Customers.FirstOrDefault(Customer => Customer.Email == emailAddress);
    }
    
    public Customer Update(Customer entity)
    {
        _context.Customers.Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}