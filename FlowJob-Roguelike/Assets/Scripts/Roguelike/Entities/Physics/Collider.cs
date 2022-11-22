using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Collider : IComponent
    {
        public bool IsTrigger = false;

        public static Collider GetColliderAtPosition(Vector2Int coord)
        {
            foreach (Entity entity in Query.Of<Collider>().With<Position>())
            {
                Position position = entity.Get<Position>();
                if (coord == position.Coord)
                {
                    return entity.Get<Collider>();
                }
            }

            return null;
        }
    }

    public class Position : IComponent
    {
        public Vector2Int Coord;
    }
}