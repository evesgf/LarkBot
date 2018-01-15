using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarkFramework.Module
{
    public class LuaModule:BusinessModule
    {
        private object m_args = null;

        internal LuaModule(string name) : base(name)
        {
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            m_args = args;

            //TODO:加载Name对应的Lua脚本
        }

        public override void Release()
        {
            base.Release();

            //TODO:释放对应的Lua脚本
        }
    }
}
