using System.Threading.Tasks;
using Core.Tools;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class PlayerFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Update()
        {
            Assets assets = Query.Single<Assets>();

            Entity entity = await new AgentFactoryJob().Run();
            entity.Add<Player>();

            AgentView view = await assets.Player.Spawn();
            view.UpdateView(entity.ToAspect<AgentAspect>());
            entity.Add(view);

            return entity;
        }
    }
}