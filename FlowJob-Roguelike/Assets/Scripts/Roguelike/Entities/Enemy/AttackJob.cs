using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class AttackJob : Job<Void, (AgentAspect agentAspect, Entity target)>
    {
        protected override async Task<Void> Run((AgentAspect agentAspect, Entity target) args)
        {
            Entity target = args.target;
            AgentAspect agentAspect = args.agentAspect;

            if (target.Has<Player>())
            {
                await new ChangeGameHealthJob().Run(-1);
            }
            if (target.TryGet(out Health health))
            {
                health.Value--;
            }

            return default;
        }
    }
}