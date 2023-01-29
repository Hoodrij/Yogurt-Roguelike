namespace FlowJob
{
    public unsafe partial struct Entity : IUnmanaged<Entity>
    {
        public void SetParent(Entity parentEntity)
        {
            this.DebugParentToSelf(parentEntity);
            
            Meta->Parent = parentEntity;
            parentEntity.Meta->Childs.Add(this);
        }

        public void UnParent()
        {
            EntityMeta* meta = Meta;
            if (meta->Parent == Null) return;
            
            meta->Parent.Meta->Childs.Remove(this);
            meta->Parent = Null;
        }

        public void Initialize()
        {
            this = default;
        }
        public void Dispose() { }
    }
}