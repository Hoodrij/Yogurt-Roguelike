using System;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using Entities.TurnSystem;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class EnemyFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Update()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();

            int minPos = data.PlayerStartPosition.x + 2; 
            int maxPos = data.BoardSize.x - minPos - 1; 
            Vector2Int position = (minPos..maxPos).GetRandomVector2Int();
            
            AgentAspect agentAspect = await new AgentFactoryJob().Run();
            agentAspect.Agent.MoveJob = new GetEnemyMoveJob();
            agentAspect.Position.Coord = position;
            agentAspect.Health.Value = data.EnemyHealth;

            AgentView view = await assets.Enemy.Spawn();
            agentAspect.Add(view);
            view.UpdateView(agentAspect);
            
            return agentAspect.Entity;
        }
    }
}