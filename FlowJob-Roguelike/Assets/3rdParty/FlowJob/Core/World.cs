using System.Collections.Generic;

namespace FlowJob
{
    internal unsafe class World
    {
        private static World instance;
        internal interface Accessor
        {
            internal World World => instance;
        }

        internal PostProcessor PostProcessor = new();
        internal UnsafeSpan<EntityMeta> EntitiesMetas = new(Consts.SIZE_ENTITIES);
        internal ManagedMetasList EntitiesManaged = new(Consts.SIZE_ENTITIES);
        internal HashSet<Entity> Entities = new(Consts.SIZE_ENTITIES);
        internal Queue<Entity> ReleasedEntities = new(Consts.SIZE_ENTITIES);
        
        private int nextEntityID = 1;

        private World()
        {
            Storage.Initialize();

#if UNITY_2020_1_OR_NEWER
            UnityEngine.Application.quitting += Dispose;
#endif
        }

        internal static Entity CreateEntity()
        {
            instance ??= new World();
            
            Entity entity;
            if (instance.ReleasedEntities.Count > 0)
            {
                entity = instance.ReleasedEntities.Dequeue();
            }
            else
            {
                entity = new()
                {
                    ID = instance.nextEntityID++,
                    Age = 0
                };
            }

            EntityMeta* meta = instance.EntitiesMetas.Get(entity.ID);
            meta->Age = entity.Age;
            meta->IsAlive = true;
            meta->ComponentsMask.Clear();
            meta->GroupsAmount = 0;

            instance.Entities.Add(entity);

            return entity;
        }

        private void Dispose()
        {
            instance = null;
#if UNITY_2020_1_OR_NEWER
            UnityEngine.Application.quitting -= Dispose;
#endif

            Entities.Clear();
            ReleasedEntities.Clear();
            PostProcessor.Clear();

            Group.Cache.Clear();
            foreach (Group group in Group.Cache.Values)
            {
                group.Dispose();
            }
            
            for (int i = 0; i < EntitiesMetas.Length; i++)
            {
                EntitiesMetas.Get(i)->Dispose();
            }
            EntitiesMetas.Dispose();
            EntitiesManaged.Clear();
            AspectCache.Clear();
        }
    }
}