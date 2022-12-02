using System;
using System.Collections.Generic;
using FlowJob;
using Tools;
using UnityEngine;

namespace Roguelike.Entities
{
    [CreateAssetMenu]
    public class Data : ScriptableObject, IComponent
    {
        public Vector2Int BoardSize;
        public Vector2Int PlayerStartPosition;
        public Vector2Int ExitPosition => BoardSize - Vector2Int.one - Vector2Int.one;
        
        public Range EnemySpawnRange
        {
            get
            {
                int minPos = PlayerStartPosition.x + 1; 
                int maxPos = BoardSize.x - 1;

                return (minPos..maxPos);
            }
        }
        
        public Range FoodSpawnRange
        {
            get
            {
                int minPos = PlayerStartPosition.x + 1; 
                int maxPos = BoardSize.x - 1;

                return (minPos..maxPos);
            }
        }

        public int StartingPlayerHealth;
        public int EnemyHealth;

        public List<Sprite> FloorSprites;
        public List<Sprite> WallSprites;
        public List<Sprite> RockSprites;

        public List<FoodData> Foods;
    }

    [Serializable]
    public struct FoodData
    {
        public int Amount;
        public Sprite Sprite;
    }
}