using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Board : IComponent
    {
        public Cell[,] Cells;

        public void SetCell(Vector2Int coord, Entity content)
        {
            Cells[coord.x, coord.y].Content = content;
        }
    }
}