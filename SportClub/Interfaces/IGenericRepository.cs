using System.Collections.Generic;

namespace SportClub.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        bool Create(T entity);

        bool Delete(int id);

        T Get(int id);

        List<T> GetAll();

        bool Update(T entity);
    }
}
