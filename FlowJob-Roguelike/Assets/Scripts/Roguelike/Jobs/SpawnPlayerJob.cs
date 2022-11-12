using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class SpawnPlayerJob : Job<Entity>
    {
        protected override async UniTask<Entity> Run()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Vector2Int coord = Vector2Int.one;
            Entity playerEntity = Level.Create()
                .Add<Player>()
                .Add<Actor>()
                .Add<Collider>()
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(new Health
                {
                    Value = data.StartingPlayerHealth
                });

            PlayerView playerView = await assets.Player.Spawn();
            playerEntity.Add(playerView);

            return playerEntity;
        }
    }
}