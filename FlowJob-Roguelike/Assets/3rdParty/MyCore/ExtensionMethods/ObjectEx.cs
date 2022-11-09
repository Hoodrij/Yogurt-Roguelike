using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ObjectEx
{
    public static string Color(this object text, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
    }

    public static string Bold(this object text)
    {
        return $"<b>{text}</b>";
    }

    public static string Italic(this object text)
    {
        return $"<i>{text}</i>";
    }

    public static void log(this object o, string tag = "")
    {
        Debug.Log(tag + o);
    }

    public static void logClear(this object o, string tag = "")
    {
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

        o.log(tag);
#endif
    }
}