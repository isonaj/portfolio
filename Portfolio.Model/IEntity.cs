using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Model
{
    public class IEntity<T>
    {
        T Id { get; }
    }
}
