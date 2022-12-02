using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;

namespace Roguelike.Entities
{
    public class EnemyMoveJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            agentAspect.MoveBy(GetMoveDirection());
            
            Direction GetMoveDirection()
            {
                Position enemyPos = agentAspect.PhysBodyAspect.Position;

                IEnumerable<Direction> freeDirectionsAround = Physics.GetFreeDirectionsAround(enemyPos.Coord);
                if (freeDirectionsAround.IsEmpty())
                    return Direction.Random;
                return freeDirectionsAround.GetRandom();
            }

            return default;
        }
    }
}