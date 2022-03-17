using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TechTest
{
    public class Repository<T> : IRepository<T> where T : Storeable
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public T FindById(IComparable id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }
        
        public void Delete(IComparable id)
        {
            _entities.Remove(FindById(id));
        }        

        public void Save(T item)
        {
            _entities.Add(item);
        }
    }
}
