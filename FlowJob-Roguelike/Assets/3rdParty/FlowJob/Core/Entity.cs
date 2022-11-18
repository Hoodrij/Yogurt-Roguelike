using System;
using System.Diagnostics;
using System.Linq;

namespace FlowJob
{
    [DebuggerDisplay("{Name}")]
    [DebuggerTypeProxy(typeof(EntityEx.EntityDebugView))]
    public unsafe partial struct Entity : IComparable<Entity>, IEquatable<Entity>
    {
        public static Entity Null = new Entity { ID = -1, Age = -1 };
        
        public int ID;
        internal int Age;

        public bool Alive => ID > 0 && Meta->IsAlive && Meta->Age == Age;

        public static Entity Create()
        {
            return World.CreateEntity();
        }

        private Entity(int id)
        {
            ID = id;
            Age = 0;
        }

        public override int GetHashCode()
        {
            return ((ID << 5) + ID) ^ Age;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Entity other)
        {
            return ID.CompareTo(other.ID);
        }

        public bool Equals(Entity other)
        {
            return ID == other.ID && Age == other.Age;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity other && Equals(other);
        }

        public static implicit operator int(Entity entity)
        {
            return entity.ID;
        }

        public static implicit operator Entity(int id)
        {
            return new Entity(id);
        }

        public static bool operator ==(Entity entity1, Entity entity2)
        {
            return entity1.ID == entity2.ID && entity1.Age == entity2.Age;
        }

        public static bool operator !=(Entity entity1, Entity entity2)
        {
            return !(entity1 == entity2);
        }

        private string Name
        {
            get
            {
                if (this == Null)
                    return "Entity.Null";
                string components = string.Concat(this.GetComponents().Select(c => $"{c.GetType().Name} ").ToArray());
                return ID + (Alive ? " " : " [DEAD] ") + components;
            }
        }
    }
}