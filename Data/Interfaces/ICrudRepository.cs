using Data.Models;

namespace Data.Interfaces;

public interface ICrudRepository<T>
{
    bool Create(T entity);
    List<T> GetAll();
    T Get(int id);
    T Update(T entity);
    void Delete(int id);
    T GetByEmailAddress(string customerEmail);
}