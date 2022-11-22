using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

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
    
    public class Collider : IComponent
    {
        public bool IsTrigger = false;

        public static IEnumerable<Vector2Int> GetFreeCoordsAround(Vector2Int origin)
        {
            Dictionary<Vector2Int,PhysBodyAspect> bodies = GetAllBodies();

            Vector2Int up = origin + Direction.Up;
            Vector2Int down = origin + Direction.Down;
            Vector2Int right = origin + Direction.Right;
            Vector2Int left = origin + Direction.Left;

            Vector2Int[] array = { up, down, right, left };
            return array.Where(point => !bodies.ContainsKey(point)).Select(point => point - origin);
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

    public class Position : IComponent
    {
        public Vector2Int Coord;
    }
}