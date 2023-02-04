using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Yogurt.Roguelike
{
    public static class Physics
    {
        public static bool CanMoveAt(Vector2Int point, PhysBodyAspect body)
        {
            CollisionLayer layerAtPoint = Physics.LayerAtPoint(point);
            return body.Collider.CanMoveAt.HasFlag(layerAtPoint);
        }
        
        public static CollisionLayer LayerAtPoint(Vector2Int point)
        {
            CollisionLayer result = 0;
            IEnumerable<Collider> collidersAtPoint = GetCollidersAtPosition(point);
            
            foreach (Collider collider in collidersAtPoint)
            {
                result |= collider.Layer;
            }

            return result;
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

        public static IEnumerable<Collider> GetCollidersAtPosition(Vector2Int coord)
        {
            foreach (PhysBodyAspect body in Query.Of<PhysBodyAspect>())
            {
                if (body.Position.Value == coord) 
                    yield return body.Collider;
            }
        }
        
        public static IEnumerable<Entity> GetEntitiesAtPosition(Vector2Int coord)
        {
            foreach (PhysBodyAspect body in Query.Of<PhysBodyAspect>())
            {
                if (body.Position.Value == coord) 
                    yield return body.Entity;
            }
        }
    }
}