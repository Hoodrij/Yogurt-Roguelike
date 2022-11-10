using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class SpawnPlayerJob : Job<Entity>
    {
        protected override async UniTask<Entity> Run()
        {
            Data data = Aspect<GameAspect>.Single().Data;
            LevelAspect levelAspect = Aspect<LevelAspect>.Single();

            Vector2Int coord = Vector2Int.one;
            Entity entity = Entity.Create()
                .Add<Player>()
                .Set(new Actor
                {
                    Coord = coord
                })
                .Set(new Health
                {
                    Value = data.StartingPlayerHealth
                });
            
            levelAspect.Board.SetCell(coord, entity);

            return entity;
        }
    }
}