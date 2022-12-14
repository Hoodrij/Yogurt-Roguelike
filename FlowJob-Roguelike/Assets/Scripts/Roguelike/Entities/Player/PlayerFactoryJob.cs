using System.Threading.Tasks;
using Core.Tools;
using Entities;
using Entities.Player;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Abilities;
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
                Abilities = Ability.PlayerAbilities,
                TurnJob = new PlayerTurnJob(),
                Layer = CollisionLayer.Destructible,
                CanMoveAt = CollisionLayer.Empty | CollisionLayer.Interactable | CollisionLayer.Destructible,
                Position = data.PlayerStartPosition,
                ViewRef = assets.Player,
            });
            
            agentAspect.Add<Player>();
            agentAspect.Set(game.Health);

            agentAspect.Get<Health>().OnHealthChangedJob = new UpdateGameUIJob();

            return agentAspect.Entity;
        }
    }
}