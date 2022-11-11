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
            public bool Exist => entity.Exist;
            public int Age => entity.Age;
            public List<IComponent> Components => entity.GetComponents();
            public Entity Parent => entity.Managed.Parent;
            public List<Entity> Childs => entity.Managed.Childs.ToList();

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

        [Conditional("UNITY_EDITOR")]
        internal static void DebugCheckExist(this Entity entity)
        {
            Debug.Assert(entity.Exist, $"Entity_{entity} does not Exist");
        }

        [Conditional("UNITY_EDITOR")]
        internal static unsafe void DebugNoComponent<T>(this Entity entity) where T : IComponent
        {
            bool entityHasComponent = entity.Meta->ComponentsMask.Has(ComponentID.Of<T>());
            Debug.Assert(entityHasComponent, $"Entity_{entity} does not have [{typeof(T).Name}]");
        }

        [Conditional("UNITY_EDITOR")]
        internal static unsafe void DebugAlreadyHave<T>(this Entity entity) where T : IComponent
        {
            bool entityHasComponent = entity.Meta->ComponentsMask.Has(ComponentID.Of<T>());
            Debug.Assert(!entityHasComponent, $"Entity_{entity} already have [{typeof(T).Name}]");
        }
        
        [Conditional("UNITY_EDITOR")]
        internal static void DebugParentToSelf(this Entity entity, Entity parent)
        {
            Debug.Assert(entity != parent, $"Entity_{entity} trying parent self");
        }
    }
}