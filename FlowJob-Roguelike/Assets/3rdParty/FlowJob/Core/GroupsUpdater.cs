using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FlowJob
{
    internal unsafe class GroupsUpdater : World.Accessor
    {
        private Queue<EntityOperation> operations = new();

        internal void Enqueue(Action action, Entity entity, ComponentID componentId = default)
        {
            operations.Enqueue(new EntityOperation
            {
                entity = entity,
                componentId = componentId,
                action = action
            });
        }

        internal void Clear() => operations.Clear();

        internal void Update()
        {
            while (operations.Count > 0)
            {
                EntityOperation operation = operations.Dequeue();
                Entity entity = operation.entity;
                EntityMeta* meta = this.GetMeta(entity);
                
                switch (operation.action)
                {
                    case Action.ComponentsChanged:
                        {
                            if (meta->ComponentsMask.IsEmpty)
                            {
                                entity.Kill();
                            }
                            
                            Stack<Group> groups = Storage.All[operation.componentId].Groups;
                            foreach (Group group in groups)
                            {
                                group.ProcessEntity(entity);
                            }

                            if (meta->ComponentsMask.IsEmpty)
                            {
                                entity.Kill();
                            }
                        }
                        break;
                    case Action.Kill:
                        {
                            for (int i = 0; i < meta->GroupsAmount; i++)
                            {
                                Group.Cache.TryGetValue(meta->Groups[i], out Group group);
                                group?.TryRemove(entity);
                            }
                            
                            meta->GroupsAmount = 0;
                            meta->ComponentsMask.Clear();
                            this.RemoveEntity(entity);
                            entity.Age += 1;
                            entity.Age %= int.MaxValue;
                        }
                        break;
                }
            }
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        private struct EntityOperation
        {
            public Entity entity;
            public ComponentID componentId;
            public Action action;
        }

        internal enum Action : byte
        {
            ComponentsChanged,
            Kill,
        }
    }
}