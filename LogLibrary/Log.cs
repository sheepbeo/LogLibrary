using UnityEngine;
using System;


public class Log
{

    private enum LogLevel
    {
        Info, Warning, Error
    }

    public static bool AllowLogging = true;
    public static bool AllowFiltering = false;
    public static string[] FilterStrings = new string[]{};

    #region Public Static Logging Methods

    public static void Info(params System.Object[] objects)
    {
        CreateLogEntry(objects, LogLevel.Info);
    }

    public static void Warning(params System.Object[] objects)
    {
        CreateLogEntry(objects, LogLevel.Warning);
    }

    public static void Error(params System.Object[] objects)
    {
        CreateLogEntry(objects, LogLevel.Error);
    }

    #endregion

    #region private Implementations

    private static void CreateLogEntry(System.Object[] objects, LogLevel level)
    {
        if (AllowLogging && objects.Length != 0)
        {
            string msg = getMessage(objects);

            if (filterAllow(msg))
            {
                unityDebug(msg, level);
            }
        }
    }

    private static string getMessage(System.Object[] objects)
    {
        string msg = "";

        foreach (System.Object o in objects)
        {
            if (o == null)
            {
                msg += "[null]";
            }
            else
            {
                msg += o.ToString();
            }
        }

        return msg;
    }

    private static bool filterAllow(string msg)
    {
        if (AllowFiltering)
        {
            if (FilterStrings != null && FilterStrings.Length > 0)
            {
                foreach (var filter in FilterStrings)
                {
                    if (msg.Contains(filter))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        return true;
    }

    private static void unityDebug(string msg, LogLevel level)
    {
        if (level == LogLevel.Info)
        {
            Debug.Log(msg);
        }
        else if (level == LogLevel.Warning)
        {
            Debug.LogWarning(msg);
        }
        else if (level == LogLevel.Error)
        {
            Debug.LogError(msg);
        }
    }

    #endregion

}
