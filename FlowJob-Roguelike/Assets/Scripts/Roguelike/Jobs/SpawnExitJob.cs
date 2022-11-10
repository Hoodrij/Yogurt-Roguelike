using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class SpawnExitJob : Job<Entity>
    {
        protected override async UniTask<Entity> Run()
        {
            Data data = Aspect<GameAspect>.Single().Data;

            Vector2Int coord = data.BoardSize - Vector2Int.one - Vector2Int.one;
            Entity entity = Entity.Create()
                .Add<Exit>()
                .Add<Collider>()
                .Add<TriggerBody>()
                .Add(new Position
                {
                    Coord = coord
                });
            
            return entity;
        }
    }
}