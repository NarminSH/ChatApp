using System;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Implementation;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    protected DbSet<T> _dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();

    }
    public async Task<bool> AddAsync(T entity)
    {
        _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
        //todo add if else statement
    }

    public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return true;
    }


    public bool Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
        return true;
        //todo add if else statement
    }

    public async Task<T> GetById(int id)
    {
        var result = await _dbSet.FindAsync(id);
        return result;
        //todo add if else statement
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _dbSet.AsNoTracking().ToListAsync();
        return result;
        //todo add if else statement
    }

    public bool Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
        //_context.Entry
        //todo save
        return true;
    }
}

