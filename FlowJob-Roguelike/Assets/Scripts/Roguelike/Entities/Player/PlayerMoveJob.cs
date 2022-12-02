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
            Direction direction = await new GetPlayerInputJob().Run();
            await new ChangeHealthJob().Run(-1);
            return direction;
        }
    }
}