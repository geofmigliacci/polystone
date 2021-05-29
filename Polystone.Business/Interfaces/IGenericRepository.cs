using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polystone.Business.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public DbSet<T> DbSet();
        public void Add(T entity);
        public void AddRange(IEnumerable<T> entities);
        public IEnumerable<T> Where(Expression<Func<T, bool>> expression);
        public bool Exist(Expression<Func<T, bool>> expression);
        public int Count(Expression<Func<T, bool>> expression);
        public decimal Sum(Expression<Func<T, decimal>> expression);
        public IEnumerable<T> GetAll();
        public T GetById(ulong id);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entities);
        public EntityEntry<T> Update(T entity);
        public int SaveChanges();
    }
}
