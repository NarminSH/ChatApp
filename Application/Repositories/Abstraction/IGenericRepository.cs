using System;
namespace Application.Repositories.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        bool Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T>  GetById(string id);
        Task<T> GetByIdInt(int id);
        Task<bool> Update(T entity);
    }
}

