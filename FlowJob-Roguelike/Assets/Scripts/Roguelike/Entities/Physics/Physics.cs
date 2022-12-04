using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools.ExtensionMethods;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike
{
    public static class Physics
    {
        public static IEnumerable<Direction> GetDirectionsAround(Vector2Int origin, CollisionLayer layer)
        {
            1.log();
            foreach (Direction direction in Direction.All)
            {
                Vector2Int newPoint = origin + direction;
                if (CanMoveAtPoint(newPoint, layer))
                {
                    yield return direction;
                    direction.log();
                }
            }
        }
        
        /// <param name="layer">Layer you are at</param>
        public static bool CanMoveAtPoint(Vector2Int point, CollisionLayer layer)
        {
            IEnumerable<Collider> collidersToMoveAt = GetColliderAtPosition(point);
            return collidersToMoveAt.All(other => layer.HasFlag(other.Layer));
        }

        public static IEnumerable<Vector2Int> GetFreeCoords(Range range)
        {
            bool noOtherBodies = Query.Of<PhysBodyAspect>().IsEmpty();

            for (int x = range.Start.Value; x < range.End.Value; x++)
            {
                for (int y = range.Start.Value; y < range.End.Value; y++)
                {
                    Vector2Int point = new Vector2Int(x,y);
                    if (noOtherBodies)
                        yield return point;
                        
                    if (Query.Of<PhysBodyAspect>().All(body => body.Position.Value != point))
                        yield return point;
                }
            }
        }

        public static IEnumerable<Collider> GetColliderAtPosition(Vector2Int coord)
        {
            foreach (PhysBodyAspect body in Query.Of<PhysBodyAspect>())
            {
                if (body.Position.Value == coord) 
                    yield return body.Collider;
            }
        }
        
        public static IEnumerable<Entity> GetEntitiesAtPosition(Vector2Int coord)
        {
            return Query.Of<PhysBodyAspect>()
                .Where(body => body.Position.Value == coord)
                .Select(body => body.Entity);
        }
    }
}