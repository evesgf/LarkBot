using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object= System.Object;

public class Debuger {

    public static bool EnableLog;
    public static bool EnableTime = true;
    public static bool EnableSave = false;
    public static bool EnableStack = false;
    public static string LogFileDir = Application.persistentDataPath + "/DebugerLog/";
    public static string LogFileName = "";
    public static string Prefix = ">>>";    //前缀
    public static StreamWriter LogFileWriter = null;

    public static void Log(object msg)
    {
        if (!EnableLog) return;

        Debug.Log(Prefix + msg);
        LogToFile("[I]" + msg);
    }

    public static void Log(object msg, Object context)
    {
        if (!EnableLog) return;

        Debug.Log(Prefix + msg);
        LogToFile("[I]" + msg);
    }

    public static void Log(object obj, string msg)
    {
        if (!EnableLog) return;

        Debug.Log(Prefix + obj+":"+msg);
        LogToFile("[I]" + msg);
    }

    public static void LogError(object msg)
    {
        //todo
    }

    public static void LogError(object msg, Object context)
    {
        //todo
    }

    public static void LogWarning(object msg)
    {
        //todo
    }

    public static void LogWarning(object msg, Object context)
    {
        //todo
    }

    //---------------------------------------------------------

    public static void Log(string tag, string msg)
    {
        if (!EnableLog) return;

        msg = GetLogText(tag, msg);
        Debug.Log(Prefix + msg);
        LogToFile("[I]" + msg);
    }

    public static void Log(string tag, string format, params object[] args)
    {
        if (!EnableLog) return;

        string msg = GetLogText(tag, string.Format(format,args));
        Debug.Log(Prefix + msg);
        LogToFile("[I]" + msg);
    }

    public static void LogError(string tag, string msg)
    {
        msg = GetLogText(tag, msg);
        Debug.Log(Prefix + msg);
        LogToFile("[E]" + msg,true);
    }

    public static void LogError(string tag, string format, params object[] args)
    {
        string msg = GetLogText(tag, string.Format(format, args));
        Debug.Log(Prefix + msg);
        LogToFile("[E]" + msg,true);
    }

    public static void LogWarning(string tag, string msg)
    {
        //todo
    }

    public static void LogWarning(string tag, string format, params object[] args)
    {
        //todo
    }

    //---------------------------------------------------------------

    private static string GetLogText(string tag, string msg)
    {
        string str = "";
        if (EnableTime)
        {
            DateTime now = DateTime.Now;
            str = now.ToString("HH:mm:ss.fff") + "";
        }

        return str + tag + "::" + msg;
    }

    private static string GetLogTime()
    {
        string str = "";
        if (EnableTime)
        {
            DateTime now = DateTime.Now;
            str = now.ToString("HH:mm:ss.fff") + "";
        }

        return str;
    }

    private static void LogToFile(string msg, bool enableStatic = false)
    {

    }
}
