using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FlowJob
{
    internal unsafe class OperationsQueue : World.Accessor
    {
        internal void Update(Entity entity, ComponentID componentId = default)
        {
            EntityMeta* meta = this.GetMeta(entity);
                
            Stack<Group> groups = Storage.All[componentId].Groups;
            foreach (Group group in groups)
            {
                group.ProcessEntity(entity);
            }

            if (meta->ComponentsMask.IsEmpty)
            {
                entity.Kill();
            }
        }
    }
}