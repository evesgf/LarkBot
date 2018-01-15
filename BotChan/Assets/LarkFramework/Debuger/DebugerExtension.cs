using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = System.Object;

public static class DebugerExtension  {

    [Conditional("EnableLog")]
    public static void Log(this object obj, string msg)
    {
        if (!Debuger.EnableLog) return;

        Debuger.Log(GetLogTag(obj), (string)msg);
    }
    public static void Log(this object obj, string format, params object[] args)
    {
        if (!Debuger.EnableLog) return;

        Debuger.Log(GetLogTag(obj), string.Format(format, args));
    }

    public static void LogError(this object obj, string msg)
    {
        Debuger.Log(GetLogTag(obj), (string)msg);
    }

    public static void LogWarning(this object obj, string msg)
    {
        Debuger.Log(GetLogTag(obj), (string)msg);
    }

    private static object GetLogTag(object obj)
    {
        //TODO
        return obj;
    }
}
