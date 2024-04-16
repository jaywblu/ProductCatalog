using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repo<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
{
    private readonly TContext _context;

    public Repo(TContext context)
    {
        _context = context;
    }

    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        try
        {
            return _context.Set<TEntity>().ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual IEnumerable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return _context.Set<TEntity>().Where(expression).ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual TEntity Get(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual TEntity Update(TEntity entity, Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
            _context.Entry(entityToUpdate!).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entityToUpdate!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
            if (entity != null)
            {
                var result = _context.Remove(entity!);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public virtual bool Existing(Expression<Func<TEntity, bool>> expression)
    {
        
        try
        {
            var existing = _context.Set<TEntity>().Any(expression);
            return existing;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}