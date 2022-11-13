using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Roguelike.Entities
{
    public struct Direction
    {
        public static Direction Random => all.GetRandom();
        
        public static readonly Direction Right = new Direction(Vector2Int.right);
        public static readonly Direction Left = new Direction(Vector2Int.left);
        public static readonly Direction Up = new Direction(Vector2Int.up);
        public static readonly Direction Down = new Direction(Vector2Int.down);
        
        private static Direction[] all = { Right, Left, Up, Down };

        private Vector2Int value;

        private Direction(Vector2Int vector)
        {
            value = vector;
        }
        
        public static implicit operator Vector2Int(Direction dir)
        {
            return dir.value;
        }

        public static implicit operator Direction(Vector2Int vector)
        {
            return new Direction(vector);
        }
    }
}