using System;
using System.Collections.Generic;

namespace FlowJob
{
    public readonly struct ComponentID
    {
        private static Dictionary<Type, ComponentID> ComponentsIds = new();

        private readonly byte ID;

        private ComponentID(byte id)
        {
            ID = id;
        }
        
        public static ComponentID Of(Type type)
        {
            if (ComponentsIds.TryGetValue(type, out ComponentID componentType))
            {
                return componentType;
            }

            componentType = new ComponentID((byte)(ComponentsIds.Values.Count));
            ComponentsIds.Add(type, componentType);
            return componentType;
        }
        
        public static ComponentID Of<T>() where T : IComponent => Of(typeof(T));

        public static implicit operator byte(ComponentID id)
        {
            return id.ID;
        }

        public static implicit operator ComponentID(byte id)
        {
            return new ComponentID(id);
        }
    }
}