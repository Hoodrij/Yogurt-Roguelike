using System;
using System.Reflection;
using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class GameObjectEx
    {
        public static T Duplicate<T>(this T source, GameObject go = null) where T : Component
        {
            if (go == null)
                go = source.gameObject;

            T newObj = go.AddComponent<T>();
            Type type = source.GetType();

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                 BindingFlags.Default |
                                 BindingFlags.DeclaredOnly | BindingFlags.FlattenHierarchy;

            if (source is MonoBehaviour)
            {
                FieldInfo[] fields = type.GetFields(flags);

                foreach (FieldInfo finfo in fields)
                    finfo.SetValue(newObj, finfo.GetValue(source));
            }
            else
            {
                PropertyInfo[] props = type.GetProperties(flags);

                foreach (PropertyInfo property in props)
                {
                    if (!property.CanWrite || !property.CanRead)
                        continue;

                    if (property.GetCustomAttributes(typeof(ObsoleteAttribute), true).Length > 0)
                        continue;

                    try
                    {
                        object val = property.GetValue(source, null);
                        property.SetValue(newObj, val, null);
                    }
                    catch { }

                    // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }

            return newObj;
        }

        public static void SetLayer(this GameObject @this, string layer, bool recursively = true)
        {
            @this.layer = LayerMask.NameToLayer(layer);

            if (recursively)
                foreach (Transform child in @this.transform)
                    child.gameObject.SetLayer(layer);
        }

        public static T GetCopyOf<T>(this Component comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                 BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Static |
                                 BindingFlags.CreateInstance;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (PropertyInfo pinfo in pinfos)
                if (pinfo.CanWrite)
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.

            FieldInfo[] finfos = type.GetFields();
            foreach (FieldInfo finfo in finfos) finfo.SetValue(comp, finfo.GetValue(other));

            return comp as T;
        }

        public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            return go.AddComponent<T>().GetCopyOf(toAdd) as T;
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T result = go.GetComponent<T>();
            if (result == null)
                result = go.AddComponent<T>();

            return result;
        }

        public static void Destroy(this GameObject go)
        {
            UnityEngine.Object.Destroy(go);
        }
    }
}