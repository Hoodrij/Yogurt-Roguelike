using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FlowJob
{
    public class Group : World.Accessor, IEnumerable<Entity>, IEquatable<Group>, IComparable<Group>
    {
        internal static Dictionary<HashCode, Group> Cache = new();

        private HashSet<Entity> entities = new(Consts.SIZE_ENTITIES);
        private readonly Composition composition;

        public static Group GetGroup(Composition composition)
        {
            if (Cache.TryGetValue(composition.Hash, out Group group))
            {
                return group;
            }

            return new Group(composition);
        }

        private Group(Composition composition)
        {
            this.composition = composition;
            Cache.Add(composition.Hash, this);

            foreach (ComponentID componentID in composition.GetIds())
            {
                Storage.All[componentID].Groups.Push(this);
            }
            
            foreach (Entity entity in this.GetEntities())
            {
                ProcessEntity(entity);
            }
        }
        
        public void Dispose()
        {
            entities.Clear();
        }
        
        public Entity Single()
        {
            this.ExecuteOperations();
            return entities.FirstOrDefault();
        }

        internal unsafe void ProcessEntity(in Entity entity)
        {
            EntityMeta* meta = entity.Meta;
            
            if (composition.Fits(entity))
            {
                if (TryAdd(entity))
                {
                    meta->AddGroup(GetHashCode());
                }
            }
            else
            {
                if (TryRemove(entity))
                {
                    meta->RemoveGroup(GetHashCode());
                }
            }
        }

        private bool TryAdd(Entity entity)
        {
            return entities.Add(entity);
        }

        internal bool TryRemove(Entity entity)
        {
            return entities.Remove(entity);
        }

        #region EQUALS

        public bool Equals(Group other)
        {
            return GetHashCode() == other?.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Group) obj);
        }

        public override int GetHashCode()
        {
            return composition.Hash;
        }

        #endregion

        #region ENUMERATOR

        public IEnumerator<Entity> GetEnumerator()
        {
            this.ExecuteOperations();
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public int CompareTo(Group other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return GetHashCode().CompareTo(other.GetHashCode());
        }
    }
}