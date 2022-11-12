using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace FlowJob
{
    public static class EntityEx
    {
        internal class EntityDebugView
        {
            public int ID => entity.ID;
            public bool Alive => entity.Alive;
            public int Age => entity.Age;
            public List<IComponent> Components => entity == Entity.Null ? new(): entity.GetComponents();
            public Entity Parent => entity.Managed.Parent;
            public List<Entity> Childs => entity == Entity.Null ? new() : entity.Managed.Childs.ToList();

            private Entity entity;
            
            public EntityDebugView(Entity entity)
            {
                this.entity = entity;
            }
        }

        internal static unsafe List<IComponent> GetComponents(this Entity entity)
        {
            List<IComponent> result = new();
            EntityMeta* meta = entity.Meta;
            foreach (byte componentId in meta->ComponentsMask.GetBytes())
            {
                Storage storage = Storage.All[componentId];
                result.Add(storage.ComponentsArray[entity]);
            }

            return result;
        }

        public static T ToAspect<T>(this Entity entity) where T : struct, Aspect<T>
        {
            return new T { Entity = entity };
        }
        
        public static Entity FromAspect(this Entity entity, Aspect aspect)
        {
            return aspect.Entity;
        }
        
        internal static bool DebugCheckNull(this Entity entity)
        {
#if UNITY_EDITOR
            if (entity == Entity.Null)
            {
                Debug.LogError($"Entity is Null");
                return true;
            }

#endif
            return false;
        }

        [Conditional("UNITY_EDITOR")]
        internal static void DebugCheckAlive(this Entity entity)
        {
            if (DebugCheckNull(entity)) return;
            if (!entity.Alive)
            {
                Debug.LogError($"{entity} does not Exist");
            }
        }

        [Conditional("UNITY_EDITOR")]
        internal static unsafe void DebugNoComponent<T>(this Entity entity) where T : IComponent
        {
            bool entityHasComponent = entity.Meta->ComponentsMask.Has(ComponentID.Of<T>());
            if (!entityHasComponent)
            {
                Debug.LogError($"{entity} does not have [{typeof(T).Name}]");
            }
        }

        [Conditional("UNITY_EDITOR")]
        internal static unsafe void DebugAlreadyHave<T>(this Entity entity) where T : IComponent
        {
            bool entityHasComponent = entity.Meta->ComponentsMask.Has(ComponentID.Of<T>());
            if (entityHasComponent)
            {
                Debug.LogError($"{entity} already have [{typeof(T).Name}]");
            }
        }
        
        [Conditional("UNITY_EDITOR")]
        internal static void DebugParentToSelf(this Entity entity, Entity parent)
        {
            if (entity == parent)
            {
                Debug.LogError($"{entity} trying parent self");
            }
        }
    }
}