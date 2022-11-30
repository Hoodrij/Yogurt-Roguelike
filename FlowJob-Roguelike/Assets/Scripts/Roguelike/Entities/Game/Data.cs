using System;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Data : IComponent
    {
        public readonly Vector2Int BoardSize = new Vector2Int(6,6);
        public readonly Vector2Int PlayerStartPosition = Vector2Int.one;
        public Vector2Int ExitPosition => BoardSize - Vector2Int.one - Vector2Int.one;
        public Range EnemySpawnRange
        {
            get
            {
                int minPos = PlayerStartPosition.x + 2; 
                int maxPos = BoardSize.x - 1;

                return (minPos..maxPos);
            }
        }

        public readonly int StartingPlayerHealth = 20;
        public readonly int EnemyHealth = 2;
    }
}