using System;

namespace Yogurt.Roguelike
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
        Hard = 1 << 1,
        Destructible = 1 << 2,
        Interactable = 1 << 3,
    }
}