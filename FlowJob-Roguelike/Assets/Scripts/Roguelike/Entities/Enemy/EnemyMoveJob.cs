using System.Collections.Generic;
using System.Linq;
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

            List<Direction> destructiblesAround 
                = Physics.GetDirectionsAround(enemyPos.Value, CollisionLayer.Destructible).ToList();
            if (!destructiblesAround.IsEmpty())
            {
                // "attack".log();
                return destructiblesAround.GetRandom();
            }
            
            // IEnumerable<Direction> freeDirectionsAround 
            //     = Physics.GetDirectionsAround(enemyPos.Value, agentAspect.PhysBodyAspect.Collider.CanMoveAt);
            // if (!freeDirectionsAround.IsEmpty())
            // {
            //     "move".log();
            //     return freeDirectionsAround.GetRandom();
            // }
            
            return Direction.None;
        }
    }
}