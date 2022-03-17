using System;
using System.Collections.Generic;
using System.Linq;

namespace TechTest
{
    public class Repository<T> : IRepository<T> where T : IStoreable
    {
        public Repository()
        {

        }

        // Get all
        public IEnumerable<T> All()
        {
            return null;
        }

        // Remove
        public void Delete(IComparable id)
        {

        }

        // Find
        public T FindById(IComparable id)
        {
            T test = default (T);
            return test;
        }

        // Update - needs a unit of work
        public void Save(T item)
        {

        }
    }
}
