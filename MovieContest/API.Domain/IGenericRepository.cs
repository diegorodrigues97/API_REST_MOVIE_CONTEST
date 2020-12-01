using MovieContest.Domain.Entities;
using System.Collections.Generic;

namespace MovieContest.Domain
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T FindById(long id);

        List<T> FindAll();

        IEnumerable<T> GetAll();

        bool Exists(long id);

        T Create(T entity);

        T Update(T entity);

        void Delete(long id);
    }
}
