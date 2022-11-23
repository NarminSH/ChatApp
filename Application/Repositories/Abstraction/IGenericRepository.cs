using System;
namespace Application.Repositories.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        //Task<bool> AddRangeAsync(IEnumerable<T> entities);
        bool Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(string id);
        bool Update(T entity);
    }
}

