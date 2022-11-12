using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Collider : IComponent
    {
        public bool IsTrigger = false;
    }

    public class Position : IComponent
    {
        public Vector2Int Coord;
    }
}