using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class PlayerFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Update()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Vector2Int coord = Vector2Int.one;
            Entity playerEntity = Level.Create()
                .Add<Player>()
                .Add<CurrentTurnAgent>()
                .Add<Collider>()
                .Add(new Agent
                {
                    MoveJob = new GetPlayerInputJob()
                })
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(new Health
                {
                    Value = data.StartingPlayerHealth
                });

            AgentView view = await assets.Player.Spawn();
            view.Update(playerEntity.ToAspect<AgentAspect>());
            playerEntity.Add(view);

            return playerEntity;
        }
    }
}