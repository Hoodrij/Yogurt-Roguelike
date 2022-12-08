﻿using System.Threading.Tasks;
using Core.Tools;
using Entities;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class PlayerFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Run()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();
            GameAspect game = Query.Single<GameAspect>();

            AgentAspect agentAspect = await new AgentFactoryJob().Run(new AgentFactoryJob.Args
            {
                Team = Team.Player,
                Layer = CollisionLayer.Destructible,
                CanMoveAt = CollisionLayer.Empty | CollisionLayer.Interactable | CollisionLayer.Destructible,
                TurnJob = new PlayerTurnJob(),
                Position = data.PlayerStartPosition,
                ViewRef = assets.Player
            });
            
            agentAspect.Add<Player>();
            agentAspect.Set(game.Health);

            return agentAspect.Entity;
        }
    }
}