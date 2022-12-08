using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;

namespace Roguelike.Entities
{
    public class EnemyMoveJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            // Position enemyPos = agentAspect.PhysBodyAspect.Position;
            //
            // List<Direction> destructiblesAround 
            //     = Physics.GetDirectionsAround(enemyPos.Value, CollisionLayer.Destructible).ToList();
            // if (!destructiblesAround.IsEmpty())
            //     return destructiblesAround.GetRandom();
            //
            // List<Direction> freeDirectionsAround 
            //     = Physics.GetDirectionsAround2(enemyPos.Value, agentAspect.PhysBodyAspect.Collider.CanMoveAt).ToList();
            // if (!freeDirectionsAround.IsEmpty())
            //     return freeDirectionsAround.GetRandom();

            return default;
        }
    }
}