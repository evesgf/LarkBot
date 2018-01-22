/*---------------------------------------------------------------
 * 作者：evesgf    创建时间：2016-8-2 15:11:35
 * 修改：evesgf    修改时间：2016-8-2 15:11:40
 *
 * 版本：V0.0.1
 * 
 * 描述：简单对象池
 * 1.怎么把游戏对象保存进缓存池
 * 2.怎么把游戏对象从缓存池里面去出来
 * 3.如何智能删除缓存池
 * 
 * 参考：http://www.cnblogs.com/mezero/p/3955130.html
 ---------------------------------------------------------------*/

using System.Collections.Generic;
using System;

namespace LarkFramework
{
    public class ObjectPool<T> where T : class, new()
    {
        private Stack<T> m_objectStack;

        private Action<T> m_resetAction;
        private Action<T> m_onetimeInitAction;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialBufferSize">池的可能最大值</param>
        /// <param name="ResetAction">重置池</param>
        /// <param name="OnetimeInitAction">初始化新对象</param>
        public ObjectPool(int initialBufferSize, Action<T>
            ResetAction = null, Action<T> OnetimeInitAction = null)
        {
            m_objectStack = new Stack<T>(initialBufferSize);
            m_resetAction = ResetAction;
            m_onetimeInitAction = OnetimeInitAction;
        }

        public T New()
        {
            if (m_objectStack.Count > 0)
            {
                T t = m_objectStack.Pop();

                if (m_resetAction != null)
                    m_resetAction(t);

                return t;
            }
            else
            {
                T t = new T();

                if (m_onetimeInitAction != null)
                    m_onetimeInitAction(t);

                return t;
            }
        }

        public void Store(T obj)
        {
            m_objectStack.Push(obj);
        }
    }
}
