using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class SpawnPlayerJob : Job<Entity>
    {
        protected override async Task<Entity> Update()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Vector2Int coord = Vector2Int.one;
            Entity playerEntity = Level.Create()
                .Add<Player>()
                .Add<Agent>()
                .Add<CurrentTurnAgent>()
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