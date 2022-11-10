using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FlowJob
{
    internal unsafe class OperationsQueue : World.Accessor
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

        internal void Execute()
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
                            Stack<Group> groups = Storage.All[operation.componentId].Groups;
                            foreach (Group group in groups)
                            {
                                group.ProcessEntity(entity);
                            }

                            if (meta->ComponentsMask.IsEmpty)
                            {
                                goto case Action.Kill;
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
                            
                            foreach (IComponent component in entity.GetComponents())
                            {
                                if (component is IDisposable disposable)
                                {
                                    disposable.Dispose();
                                }
                            }
                            
                            meta->GroupsAmount = 0;
                            meta->ComponentsMask.Clear();
                            meta->IsAlive = false;

                            entity.Age += 1;
                            entity.Age %= int.MaxValue;
                            
                            this.RemoveEntity(entity);
                        }
                        break;
                }
            }
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        internal struct EntityOperation
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