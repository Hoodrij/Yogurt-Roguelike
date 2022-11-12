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
            Data data = Query.Single<Data>();

            Vector2Int coord = data.BoardSize - Vector2Int.one - Vector2Int.one;
            Entity entity = Level.Create()
                .Add<Exit>()
                .Add(new Collider { IsTrigger = true })
                .Add(new Position
                {
                    Coord = coord
                });
            
            return entity;
        }
    }
}