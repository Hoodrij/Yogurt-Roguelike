using System.Collections.Generic;

namespace FlowJob
{
    public unsafe partial struct Entity
    {
        internal ref EntityManagedMeta Managed => ref this.GetManagedMeta(ID);
        
        public void SetParent(Entity parentEntity)
        {
            this.DebugParentToSelf(parentEntity);
            
            Managed.Parent = parentEntity;
            parentEntity.Managed.Childs.Add(this);
        }

        public void UnParent()
        {
            Managed.Parent.Managed.Childs.Remove(this);
            Managed.Parent = default;
        }

        public HashSet<Entity> GetChilds()
        {
            return Managed.Childs;
        }
    }
    
    public struct EntityManagedMeta
    {
        internal HashSet<Entity> Childs;
        internal Entity Parent;

        public void Initialize()
        {
            Childs = new();
        }
    }
}