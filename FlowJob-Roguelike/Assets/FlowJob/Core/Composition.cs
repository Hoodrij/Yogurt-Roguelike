using System;
using System.Collections.Generic;

namespace FlowJob
{
    public struct Composition : IEquatable<Composition>
    {
        private readonly Mask included;
        private readonly Mask excluded;
        internal readonly HashCode Hash;

        public Composition(Mask included, Mask excluded) : this()
        {
            this.included = included;
            this.excluded = excluded;
            Hash = HashCode.Of(included).And(17).And(31).And(excluded);
        }
        
        internal readonly unsafe bool Fits(Entity entity)
        {
            return entity.Meta->ComponentsMask.HasAll(included)
                   && !entity.Meta->ComponentsMask.HasAny(excluded);
        }
        
        public IEnumerable<ComponentID> GetIds()
        {
            foreach (byte id in included.And(excluded).GetBytes()) 
                yield return id;
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public bool Equals(Composition other)
        {
            return GetHashCode() == other.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            return obj is Composition other && Equals(other);
        }
    }
}