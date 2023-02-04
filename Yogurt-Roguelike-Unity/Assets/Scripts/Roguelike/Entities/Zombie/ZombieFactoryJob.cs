using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Yogurt.Roguelike.Abilities;

namespace Yogurt.Roguelike
{
    public struct ZombieFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();
            
            AgentAspect agentAspect = await new AgentFactoryJob().Run(new AgentFactoryJob.Args
            {
                Team = Team.Zombie,
                Layer = CollisionLayer.Destructible,
                CanMoveAt = 0,
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