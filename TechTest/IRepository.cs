using System;
using System.Collections.Generic;

namespace TechTest
{
    // Please create an in memory implementation of IRepository<T> 

    public interface IRepository<T> where T : IStoreable
    {
        // Get all
        IEnumerable<T> All();

        // Remove
        void Delete(IComparable id);

        // Find
        T FindById(IComparable id);

        // Update - needs a unit of work
        void Save(T item);
    }
}
