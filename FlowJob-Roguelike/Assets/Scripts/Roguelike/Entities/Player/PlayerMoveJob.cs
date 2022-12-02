using System.Threading.Tasks;
using Core.Tools;
using Roguelike;
using Roguelike.Entities;
using Roguelike.Jobs;

namespace Entities.Player
{
    public class PlayerMoveJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            Direction direction = await new GetPlayerInputJob().Run();
            agentAspect.MoveBy(direction);
            
            await new ChangeHealthJob().Run(-1);

            return default;
        }
    }
}