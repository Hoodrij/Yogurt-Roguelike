using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Data : IComponent
    {
        public Vector2Int BoardSize = new Vector2Int(10, 10);
    }
}