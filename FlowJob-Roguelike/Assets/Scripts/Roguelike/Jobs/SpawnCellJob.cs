using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class SpawnCellJob : Job<Entity>
    {
        private Vector2Int coord;

        public SpawnCellJob(Vector2Int coord)
        {
            this.coord = coord;
        }

        protected override async UniTask<Entity> Run()
        {
            Entity entity = Entity.Create()
                .Set(new Cell
                {
                    Coord = coord
                });

            return entity;
        }
    }
}