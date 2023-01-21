using System.Collections.Generic;

namespace FlowJob
{
    internal static class WorldAccessor
    {
        internal static World World { get; set; }
        
        public static void Enqueue(PostProcessor.Action action, Entity entity, ComponentID componentID = default)
        {
            World.PostProcessor.Enqueue(action, entity, componentID);
        }

        public static void UpdateWorld()
        {
            World.PostProcessor.Update();
        }

        public static ref HashSet<Entity> GetEntities()
        {
            return ref World.Entities;
        }

        public static unsafe EntityMeta* GetMeta(Entity entity)
        {
            EntityMeta* entityMeta = World.EntitiesMetas.Get(entity);
            return entityMeta;
        }
        
        public static ref EntityManagedMeta GetManagedMeta(Entity entity)
        {
            return ref World.EntitiesManaged.Get(entity);
        }

        public static void RemoveEntity(Entity entity)
        {
            World.Entities.Remove(entity);
            World.ReleasedEntities.Enqueue(entity);
        }
    }
}