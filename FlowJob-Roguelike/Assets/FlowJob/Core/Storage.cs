using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlowJob
{
    public abstract class Storage
    {
        internal static Storage[] All;

        internal Stack<Group> Groups = new();

        internal static void Initialize()
        {
            All = new Storage[Consts.SIZE_COMPONENTS];
            
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.GetInterfaces().Contains(typeof(IComponent))) continue;
                    Type genericStorage = typeof(Storage<>).MakeGenericType(type);
                    Activator.CreateInstance(genericStorage);
                }
            }
        }
        
        internal abstract IComponent[] DebugComponents { get; }
    }

    public class Storage<T> : Storage where T : IComponent
    {
        internal static Storage<T> Instance;

        private T[] Components = new T[Consts.SIZE_ENTITIES / 2];

        public Storage()
        {
            Instance = this;
            ComponentID id = ComponentID.Of<T>();
            All[id] = this;
        }

        public void Add(T component, int index)
        {
            if (index >= Components.Length)
            {
                Array.Resize(ref Components, index + index / 2);
            }
            
            Components[index] = component;
        }

        public ref T Get(int index)
        {
            return ref Components[index];
        }
        
        internal override IComponent[] DebugComponents => Components as IComponent[];
    }
}