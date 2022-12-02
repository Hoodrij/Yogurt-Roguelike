using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;

namespace Roguelike.Entities
{
    public class EnemyMoveJob : Job<Direction, AgentAspect>
    {
        protected override async Task<Direction> Run(AgentAspect agentAspect)
        {
            Position enemyPos = agentAspect.PhysBodyAspect.Position;

            IEnumerable<Direction> freeDirectionsAround = Physics.GetFreeDirectionsAround(enemyPos.Value);
            return freeDirectionsAround.GetRandom();
        }
    }
}