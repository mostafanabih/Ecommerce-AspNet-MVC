using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Icollection<T>
    {
        public static implicit operator Icollection<T>(HashSet<Product> v)
        {
            throw new NotImplementedException();
        }
    }
}