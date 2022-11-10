using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class SpawnExitJob : Job<Entity>
    {
        protected override async UniTask<Entity> Run()
        {
            Data data = Aspect<GameAspect>.Single().Data;
            LevelAspect levelAspect = Aspect<LevelAspect>.Single();

            Vector2Int coord = data.BoardSize - Vector2Int.one - Vector2Int.one;
            Entity entity = Entity.Create()
                .Add<Exit>()
                .Set(new Actor
                {
                    Coord = coord
                });
            
            levelAspect.Board.SetCell(coord, entity);

            return entity;
        }
    }
}