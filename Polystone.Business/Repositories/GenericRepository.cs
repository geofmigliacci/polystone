using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Polystone.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Polystone.Business.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PolystoneContext _context;

        public GenericRepository(PolystoneContext context)
        {
            _context = context;
        }

        public DbSet<T> DbSet()
        {
            return _context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Count(expression);
        }

        public decimal Sum(Expression<Func<T, decimal>> expression)
        {
            return _context.Set<T>().Sum(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(ulong id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public EntityEntry<T> Update(T entity)
        {
            return _context.Update(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
