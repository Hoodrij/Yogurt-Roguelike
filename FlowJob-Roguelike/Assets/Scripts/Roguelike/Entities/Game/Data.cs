using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Data : IComponent
    {
        public readonly Vector2Int BoardSize = new Vector2Int(10, 10);
        public readonly int StartingPlayerHealth = 100;
    }
}