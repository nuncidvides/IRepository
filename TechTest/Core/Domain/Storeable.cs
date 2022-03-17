using System;
using System.Collections.Generic;
using System.Text;

namespace TechTest
{
    public class Storeable : IStoreable
    {
        public IComparable Id { get; set; }
    }
}
