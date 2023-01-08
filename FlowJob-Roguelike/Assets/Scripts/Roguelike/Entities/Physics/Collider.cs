using System;
using FlowJob;

namespace Roguelike
{
    public class Collider : IComponent
    {
        public static readonly Collider Hard = new Collider { Layer = CollisionLayer.Hard };
        
        public CollisionLayer Layer;
        public CollisionLayer CanMoveAt;
    }

    [Flags]
    public enum CollisionLayer
    {
        Destructible = 1 << 1,
        Hard = 1 << 2,
        Interactable = 1 << 3,
    }
}