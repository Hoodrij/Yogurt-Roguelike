using System;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Jobs;
using UnityEngine;
using Physics = Entities.Physics;

namespace Roguelike.Entities
{
    public class EnemyFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Update()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();

            AgentAspect agentAspect = await new AgentFactoryJob().Run();
            agentAspect.Agent.MoveJob = new GetEnemyMoveJob();
            agentAspect.Health.Value = data.EnemyHealth;
            agentAspect.PhysBodyAspect.Position.Coord = GetSpawnPosition();
            agentAspect.PhysBodyAspect.Collider.Layer = CollisionLayer.Destructible;
            agentAspect.PhysBodyAspect.Collider.CollisionMap = CollisionLayer.Hard | CollisionLayer.Interactable;

            AgentView view = await assets.Enemy.Spawn();
            agentAspect.Add(view);
            view.UpdateView(agentAspect);
            
            return agentAspect.Entity;

            Vector2Int GetSpawnPosition()
            {
                int minPos = data.PlayerStartPosition.x + 2; 
                int maxPos = data.BoardSize.x - minPos - 1;

                Range range = (minPos..maxPos);
                return Physics.GetFreeCoords(range).GetRandom();
            }
        }
    }
}