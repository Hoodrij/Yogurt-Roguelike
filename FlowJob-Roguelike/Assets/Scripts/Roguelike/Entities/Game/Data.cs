using System;
using System.Collections.Generic;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu]
    public class Data : ScriptableObject, IComponent
    {
        public Vector2Int BoardSize;
        public Vector2Int PlayerStartPosition;
        public Vector2Int ExitPosition => BoardSize - Vector2Int.one - Vector2Int.one;
        
        public Range SpawnRange
        {
            get
            {
                int minPos = PlayerStartPosition.x + 1; 
                int maxPos = BoardSize.x - 1;

                return (minPos..maxPos);
            }
        }

        public int FoodCount;
        public int EnemiesCount;
        public int RocksCount;

        public int StartingPlayerHealth;
        public int  ZombieHealth;

        public List<Sprite> FloorSprites;
        public List<Sprite> WallSprites;
        public List<RockData> Rocks;

        public List<FoodData> Foods;
    }
}