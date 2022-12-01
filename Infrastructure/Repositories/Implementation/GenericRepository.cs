using System;
using System.Net;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;


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
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
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
    }

    public async Task<T> GetById(string id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            return entity;
        }
        else { throw new NotFoundException(); }
    }

    public async Task<T> GetByIdInt(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity!=null)
        {
        _context.Entry(entity).State = EntityState.Detached;
        return entity;
        }
        else { throw new NotFoundException(); }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _dbSet.AsNoTracking().ToListAsync();
        return result;
        //todo add if else statement
    }
        
    public async Task<bool> Update(T entity)
    {
        _dbSet.Update(entity);
        if(await _context.SaveChangesAsync() >= 1) { return true; }
        else { return false; }

    }
}

