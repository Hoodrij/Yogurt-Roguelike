using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Cell : IComponent
    {
        public Vector2Int Coord;
        public Entity Content;
    }
    
    public struct CellAspect : Aspect<CellAspect>
    {
        public Entity Entity { get; set; }

        public Cell Cell => this.Get<Cell>();
    }
}