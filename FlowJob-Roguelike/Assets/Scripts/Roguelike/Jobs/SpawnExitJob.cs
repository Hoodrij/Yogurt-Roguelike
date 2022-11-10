using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class SpawnExitJob : Job<Entity>
    {
        private Vector2Int coord;

        public SpawnExitJob(Vector2Int coord)
        {
            this.coord = coord;
        }
        
        protected override async UniTask<Entity> Run()
        {
            Entity entity = Entity.Create()
                .Add<Exit>()
                .Set(new Actor
                {
                    Coord = coord
                });

            return entity;
        }
    }
}