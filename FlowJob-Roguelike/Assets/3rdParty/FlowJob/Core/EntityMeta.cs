using System.Runtime.InteropServices;

namespace FlowJob
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EntityMeta : IUnmanaged<EntityMeta>
    {
        internal bool IsAlive;
        internal int Id;
        internal int Age;
        internal Mask ComponentsMask;

        internal UnsafeSpan<GroupId> Groups;
        
        internal UnsafeSpan<Entity> Childs;
        public Entity Parent;

        public void Initialize()
        {
            Parent = default;
            ComponentsMask.Clear();
            Groups = new(4);
            Childs = new(4);
        }

        public void Dispose()
        {
            Parent = default;
            ComponentsMask.Clear();
            Groups.Dispose();
            Childs.Dispose();
        }
        
        public bool Equals(EntityMeta other)
        {
            return IsAlive == other.IsAlive 
                   && Age == other.Age 
                   && Id == other.Id;
        }
    }
}