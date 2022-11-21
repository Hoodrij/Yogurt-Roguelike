using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Data : IComponent
    {
        public readonly Vector2Int BoardSize = new Vector2Int(10, 10);
        public readonly Vector2Int PlayerStartPosition = Vector2Int.one;
        public Vector2Int ExitStartPosition => BoardSize - Vector2Int.one;
        public readonly int StartingPlayerHealth = 100;
        public readonly int EnemyHealth = 2;
    }
}