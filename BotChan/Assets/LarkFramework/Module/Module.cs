using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarkFramework.Module
{
    public abstract class Module
    {
        public virtual void Release()
        {
            this.Log("Release");
        }
    }
}
