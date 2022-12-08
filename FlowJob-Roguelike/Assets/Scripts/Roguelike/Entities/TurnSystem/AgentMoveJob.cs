using System.Threading.Tasks;
using Core.Tools;
using Roguelike;
using Roguelike.Entities;

namespace Entities.TurnSystem
{
    public class AgentMoveJob : Job<Void, (AgentAspect agentAspect, Direction direction)>
    {
        protected override async Task<Void> Run((AgentAspect agentAspect, Direction direction) args)
        {
            args.agentAspect.PhysBodyAspect.Position.Value += args.direction;
            args.agentAspect.View.UpdateView(args.agentAspect);

            return default;
        }
    }
}