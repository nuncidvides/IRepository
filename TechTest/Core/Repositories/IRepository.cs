using System;
using System.Collections.Generic;

namespace TechTest
{
    // Please create an in memory implementation of IRepository<T> 

    public interface IRepository<T> where T : IStoreable
    {
        IEnumerable<T> GetAll();
        T FindById(IComparable id);
        void Delete(IComparable id);
        void Save(T item);
    }
}
