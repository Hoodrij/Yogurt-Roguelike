using System;
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
        internal MemoryPool<EntityMeta> EntitiesMetas = new(Consts.SIZE_ENTITIES);
        internal ManagedMetasList EntitiesManaged = new(Consts.SIZE_ENTITIES);
        internal HashSet<Entity> Entities = new(Consts.SIZE_ENTITIES);
        internal Queue<Entity> ReleasedEntities = new(Consts.SIZE_ENTITIES);
        
        private int nextEntityID = 1;

        private World()
        {
            instance = this;
            Storage.Initialize();

#if UNITY_2020_1_OR_NEWER
            UnityEngine.Application.quitting += Dispose;
#endif
        }

        internal static Entity CreateEntity()
        {
            if (instance == null) new World();
            
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
            for (int i = 0; i < EntitiesMetas.Length; i++)
            {
                EntitiesMetas.Get(i)->Dispose();
            }

            Entities.Clear();
            ReleasedEntities.Clear();
            PostProcessor.Clear();

            foreach (Group group in Group.Cache.Values)
            {
                group.Dispose();
            }

            Group.Cache.Clear();
            EntitiesMetas.Dispose();
            EntitiesManaged.Clear();
            UnmanagedMemory.Cleanup();
            AspectCache.Clear();
        }
    }
}