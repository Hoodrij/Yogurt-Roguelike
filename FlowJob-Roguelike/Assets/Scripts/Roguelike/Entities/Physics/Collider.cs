using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Collider : IComponent
    {
        public bool IsTrigger = false;

        public static IEnumerable<Vector2Int> GetFreeCoordsAround(Vector2Int origin)
        {
            Dictionary<Vector2Int,PhysBodyAspect> bodies = GetAllBodies();
            
            foreach (Direction direction in Direction.All)
            {
                Vector2Int newPoint = origin + direction;
                if (!bodies.TryGetValue(newPoint, out var _))
                {
                    yield return direction;
                }
            }
        }

        public static IEnumerable<Vector2Int> GetFreeCoords(Range range)
        {
            Dictionary<Vector2Int,PhysBodyAspect> bodies = GetAllBodies();

            for (int x = range.Start.Value; x < range.End.Value; x++)
            {
                for (int y = range.Start.Value; y < range.End.Value; y++)
                {
                    Vector2Int point = new Vector2Int(x,y);
                    if (bodies.TryGetValue(point, out PhysBodyAspect _)) continue;

                    yield return point;
                }
            }
        }

        public static Collider GetColliderAtPosition(Vector2Int coord)
        {
            return GetAllBodies().TryGetValue(coord, out PhysBodyAspect body) 
                ? body.Collider 
                : null;
        }

        public static Dictionary<Vector2Int, PhysBodyAspect> GetAllBodies()
        {
            Dictionary<Vector2Int, PhysBodyAspect> result = new();
            foreach (PhysBodyAspect physBodyAspect in Query.Of<PhysBodyAspect>())
            {
                result.Set(physBodyAspect.Position.Coord, physBodyAspect);
            }

            return result;
        }
    }
}