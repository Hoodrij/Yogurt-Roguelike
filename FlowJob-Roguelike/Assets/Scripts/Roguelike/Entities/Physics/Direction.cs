using System;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Roguelike
{
    public struct Direction : IEquatable<Direction>
    {
        public static Direction Random => all.GetRandom();
        
        public static readonly Direction Right = new Direction(Vector2Int.right);
        public static readonly Direction Left = new Direction(Vector2Int.left);
        public static readonly Direction Up = new Direction(Vector2Int.up);
        public static readonly Direction Down = new Direction(Vector2Int.down);
        public static readonly Direction None = new Direction(Vector2Int.zero);
        
        private static Direction[] all = { Right, Left, Up, Down };

        private Vector2Int value;

        public Direction(int x, int y) : this(new Vector2Int(x, y)) { }
        
        public Direction(Vector2Int vector)
        {
            value = vector.Normalized();
        }
        
        public static implicit operator Vector2Int(Direction dir)
        {
            return dir.value;
        }

        public static implicit operator Direction(Vector2Int vector)
        {
            return new Direction(vector);
        }
        
        public static bool operator ==(Direction left, Direction right) => left.value.x == right.value.x && left.value.y == right.value.y;
        public static bool operator !=(Direction left, Direction right) => !(left == right);
        public static Vector2Int operator +(Vector2Int position, Direction direction) => position + direction.value;

        public override bool Equals(object other) => other is Direction other1 && Equals(other1);

        public bool Equals(Direction other) => this == other;

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString()
        {
            return value.ToString();
        }
    }
}