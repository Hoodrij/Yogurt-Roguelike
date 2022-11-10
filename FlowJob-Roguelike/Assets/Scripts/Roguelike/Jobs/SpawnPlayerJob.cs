using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class SpawnPlayerJob : Job<Entity>
    {
        private Vector2Int coord;

        public SpawnPlayerJob(Vector2Int coord)
        {
            this.coord = coord;
        }
        
        protected override async UniTask<Entity> Run()
        {
            Data data = Aspect<GameAspect>.Single().Data;

            Entity entity = Entity.Create()
                .Add<Player>();
            
            entity.Set(new Actor
            {
                Coord = coord
            });
            entity.Set(new Health
            {
                Value = data.StartingPlayerHealth
            });

            return entity;
        }
    }
}