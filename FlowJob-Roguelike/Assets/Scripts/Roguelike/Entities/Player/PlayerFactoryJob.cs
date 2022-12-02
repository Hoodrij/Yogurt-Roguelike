using System.Threading.Tasks;
using Core.Tools;
using Entities.Player;
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
                Layer = CollisionLayer.Destructible,
                CollisionMap = CollisionLayer.Hard,
                MoveJob = new PlayerMoveJob(),
                Position = data.PlayerStartPosition
            });
            
            agentAspect.Add<Player>();
            agentAspect.Set(game.Health);

            AgentView view = await assets.Player.Spawn();
            agentAspect.Add(view);
            view.UpdateView(agentAspect);

            return agentAspect.Entity;
        }
    }
}