﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TechTest
{
    public class Repository<T> : IRepository<T> where T : Storeable
    {
        public List<Storeable> _proxyData; // Stand-in for actual data source
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository()
        {

        }

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public T FindById(IComparable id)
        {
            return _proxyData.Find(x => x.Id == id) as T;
            //return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _proxyData.ToList() as IEnumerable<T>;
            //return _entities.ToList();
        }
        
        public void Delete(IComparable id)
        {
            _proxyData.Remove(FindById(id));
            //_entities.Remove(FindById(id));
        }        

        public void Save(T item)
        {
            _proxyData.Add(item);
            //_entities.Add(item);
        }
    }
}
