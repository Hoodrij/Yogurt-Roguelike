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
        protected override async Task<Entity> Run()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();
            
            AgentAspect agentAspect = await new AgentFactoryJob().Run(new AgentFactoryJob.Args
            {
                Layer = CollisionLayer.Destructible,
                CollisionMap = CollisionLayer.Hard | CollisionLayer.Interactable,
                MoveJob = new GetEnemyMoveJob(),
                Position = GetSpawnPosition()
            });
            agentAspect.Health.Value = data.EnemyHealth;

            AgentView view = await assets.Enemy.Spawn();
            agentAspect.Add(view);
            view.UpdateView(agentAspect);
            
            return agentAspect.Entity;

            Vector2Int GetSpawnPosition()
            {
                return Physics.GetFreeCoords(data.EnemySpawnRange).GetRandom();
            }
        }
    }
}