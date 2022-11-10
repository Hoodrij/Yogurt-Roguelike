using System.Collections.Generic;

namespace FlowJob
{
    public unsafe partial struct Entity
    {
        private ref EntityManagedMeta Managed => ref this.GetManagedMeta(ID);
        
        public void SetParent(Entity parentEntity)
        {
            Managed.parent = parentEntity;
            parentEntity.Managed.childs.Add(this);
        }

        public void UnParent()
        {
            Managed.parent.Managed.childs.Remove(this);
            Managed.parent = default;
        }

        public HashSet<Entity> GetChilds()
        {
            return Managed.childs;
        }
    }
    
    public struct EntityManagedMeta
    {
        internal HashSet<Entity> childs;
        internal Entity parent;

        public void Initialize()
        {
            childs = new();
        }
    }
}