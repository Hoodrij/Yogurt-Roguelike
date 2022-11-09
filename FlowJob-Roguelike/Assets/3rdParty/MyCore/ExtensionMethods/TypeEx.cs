using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Tools.ExtensionMethods
{
    public static class TypeEx
    {
        public static IEnumerable<KeyValuePair<PropertyInfo, T>> GetAllPropertiesWithAttribute<T>(this Type type)
            where T : Attribute
        {
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                T attribute = property.GetCustomAttribute<T>();
                if (attribute == null) continue;

                yield return new KeyValuePair<PropertyInfo, T>(property, attribute);
            }
        }

        public static IEnumerable<KeyValuePair<FieldInfo, T>> GetAllFieldsWithAttribute<T>(this Type type)
            where T : Attribute
        {
            FieldInfo[] properties = type.GetFields();
            foreach (FieldInfo property in properties)
            {
                T attribute = property.GetCustomAttribute<T>();
                if (attribute == null) continue;

                yield return new KeyValuePair<FieldInfo, T>(property, attribute);
            }
        }

        public static IEnumerable<Type> GetNotAbstractChildrenTypesWithAttribute<T>(this Type type)
            where T : Attribute
        {
            return GetAllTypes()
                .Where(type.IsAssignableFrom)
                .Where(t => !t.IsAbstract)
                .Where(t => t.HasAttribute<T>());
        }

        private static IEnumerable<Type> GetAllTypes()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            foreach (Type type in assembly.GetTypes())
                yield return type;
        }

        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>() != null;
        }

        public static IEnumerable<Type> GetLeafChildrenNotAbstractClasses(this Type type)
        {
            return GetAllTypes().Where(type.IsAssignableFrom).Where(t => t.IsClass).Where(t => !t.IsAbstract)
                .Where(t => t.IsLeafType());
        }

        public static bool IsLeafType(this Type type)
        {
            return GetAllTypes().Count(type.IsAssignableFrom) == 1;
        }

        public static FieldInfo[] GetAllInstanceFields(this Type type)
        {
            return type.GetFields(GetAllInstanceFlags());
        }

        private static BindingFlags GetAllInstanceFlags()
        {
            return BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        }

        public static MethodInfo[] GetAllInstanceMethods(this Type type)
        {
            return type.GetMethods(GetAllInstanceFlags());
        }

        public static IEnumerable<MethodInfo> GetAllMethodsWithAttribute<T>(this Type type, bool recursive = true)
            where T : Attribute
        {
            foreach (MethodInfo method in type.GetAllInstanceMethods())
            {
                if (method.GetCustomAttribute<T>() != null)
                {
                    yield return method;
                }
            }

            if (recursive && type.BaseType != null)
                foreach (MethodInfo methodInfo in type.BaseType.GetAllMethodsWithAttribute<T>(recursive))
                    yield return methodInfo;
        }
        
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        public static bool IsAssignableFromAny(this Type type, IEnumerable<Type> list)
        {
            foreach (Type other in list)
            {
                if (type.IsAssignableToGenericType(other))
                    return true;

                if (type.IsSubclassOf(other))
                    return true;
            }
            return false;
        }
    }
}