using System;
using System.Collections.Generic;
using System.Text;

public class Singleton<T> where T : Singleton<T>
{
    //实例化参数
    private static T _instance;

    //构造函数
    static Singleton()
    {
        return;
    }

    /// <summary>
    /// 实例化单例
    /// </summary>
    /// <returns></returns>
    public static T Create()
    {
        if (_instance == null)
        {
            _instance = (T)Activator.CreateInstance(typeof(T), true);
            if (_instance == null)
                throw new Exception("Singleton Instance Defeated!!");
        }

        return _instance;
    }

    /// <summary>
    /// 获取单例
    /// 此处不提供自动创建，旨在明确单例创建时间
    /// </summary>
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// 销毁单例
    /// </summary>
    public static void Destroy()
    {
        _instance = null;

        return;
    }
}
