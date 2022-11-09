using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace FlowJob
{
    internal static class EntityEx
    {
        internal class EntityDebugView
        {
            public int ID => entity.ID;
            public List<IComponent> Components => entity.GetComponents();
            
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
                result.Add(storage.DebugComponents[entity]);
            }

            return result;
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
    }
}