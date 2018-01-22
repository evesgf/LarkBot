using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarkFramework.Entity
{
    public interface IRecyclableObject
    {
        string GetRecycleType();
        void Dispose();
    }
}
