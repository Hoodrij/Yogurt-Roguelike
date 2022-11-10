﻿using System.Collections.Generic;

namespace FlowJob
{
    internal static class WorldAccessorEx
    {
        public static void Enqueue(this World.Accessor a, GroupsUpdater.Action action, Entity entity, ComponentID componentID = default)
        {
            a.World.GroupsUpdater.Enqueue(action, entity, componentID);
        }

        public static void ExecuteOperations(this World.Accessor a)
        {
            a.World.GroupsUpdater.Update();
        }

        public static ref HashSet<Entity> GetEntities(this World.Accessor a)
        {
            return ref a.World.Entities;
        }

        public static unsafe EntityMeta* GetMeta(this World.Accessor a, Entity entity)
        {
            return a.World.EntitiesMetas.Get(entity);
        }

        public static void RemoveEntity(this World.Accessor a, Entity entity)
        {
            a.World.Entities.Remove(entity);
            a.World.ReleasedEntities.Enqueue(entity);
        }
    }
}