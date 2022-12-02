using System.Threading.Tasks;
using Core.Tools;
using Roguelike;
using Roguelike.Entities;
using Roguelike.Jobs;

namespace Entities.Player
{
    public class PlayerMoveJob : Job<Direction, AgentAspect>
    {
        protected override async Task<Direction> Run(AgentAspect agentAspect)
        {
            return await new GetPlayerInputJob().Run();
        }
    }
}