using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Abilities;
using UnityEngine;

namespace Roguelike
{
    public class ZombieFactoryJob
    {
        public async UniTask<Entity> Run()
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
                Abilities = IAbility.ZombieAbilities
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