using Core.Tools;
using Cysharp.Threading.Tasks;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class PlayerFactoryJob : Job<Entity>
    {
        protected override async UniTask<Entity> Update()
        {
            Assets assets = Query.Single<Assets>();
            Data data = Query.Single<Data>();

            AgentAspect agentAspect = await new AgentFactoryJob().Run();
            agentAspect.Add<Player>();
            agentAspect.Agent.MoveJob = new GetPlayerInputJob();
            agentAspect.PhysBodyAspect.Position.Coord = data.PlayerStartPosition;
            agentAspect.PhysBodyAspect.Collider.Layer = CollisionLayer.Destructible;
            agentAspect.PhysBodyAspect.Collider.CollisionMap = CollisionLayer.Hard;
            agentAspect.Health.Value = data.StartingPlayerHealth;

            AgentView view = await assets.Player.Spawn();
            agentAspect.Add(view);
            view.UpdateView(agentAspect);

            return agentAspect.Entity;
        }
    }
}