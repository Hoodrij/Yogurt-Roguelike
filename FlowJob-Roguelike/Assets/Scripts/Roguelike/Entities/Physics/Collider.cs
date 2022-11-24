﻿using System;
using FlowJob;

namespace Roguelike.Entities
{
    public class Collider : IComponent
    {
        public static readonly Collider Empty = new Collider { Layer = CollisionLayer.Empty };
        public static readonly Collider Hard = new Collider { Layer = CollisionLayer.Hard };
        
        public CollisionLayer Layer;
        public CollisionLayer CollisionMap;

        public bool CanMoveAt(Collider other)
        {
            return !CollisionMap.HasFlag(other.Layer);
        }
    }

    [Flags]
    public enum CollisionLayer
    {
        Empty = 1 << 1,
        Destructible = 1 << 2,
        Hard = 1 << 3,
        Interactable = 1 << 4,
    }
}