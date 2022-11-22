using System;
using FlowJob;

namespace Roguelike.Entities
{
    public struct PhysBodyAspect : Aspect<PhysBodyAspect>, IEquatable<PhysBodyAspect>
    {
        public Entity Entity { get; set; }

        public Collider Collider => this.Get<Collider>();
        public Position Position => this.Get<Position>();

        public override int GetHashCode()
        {
            return Entity.GetHashCode();
        }

        public bool Equals(PhysBodyAspect other)
        {
            return Entity.Equals(other.Entity);
        }

        public override bool Equals(object obj)
        {
            return obj is PhysBodyAspect other && Equals(other);
        }
    }
}