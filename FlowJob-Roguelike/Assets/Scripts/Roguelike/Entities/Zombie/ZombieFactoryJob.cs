using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using Roguelike.Abilities;
using UnityEngine;

namespace Roguelike
{
    public class ZombieFactoryJob : Job<Task<Entity>>
    {
        public override async Task<Entity> Run()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();
            
            AgentAspect agentAspect = await new AgentFactoryJob().Run(new AgentFactoryJob.Args
            {
                Team = Team.Zombie,
                Layer = CollisionLayer.Destructible,
                CanMoveAt = CollisionLayer.Empty,
                TurnJob = new ZombieTurnJob(),
                Position = GetSpawnPosition(),
                ViewRef = assets.Zombie,
                Abilities = Ability.ZombieAbilities
            });
            agentAspect.Health.Value = data.ZombieHealth;

            return agentAspect.Entity;

            Vector2Int GetSpawnPosition()
            {
                return Physics.GetFreeCoords(data.SpawnRange).GetRandom();
            }
        }
    }
}