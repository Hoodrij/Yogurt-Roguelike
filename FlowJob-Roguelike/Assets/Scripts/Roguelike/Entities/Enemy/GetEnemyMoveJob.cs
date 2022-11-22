using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using Physics = Entities.Physics;

namespace Roguelike.Entities
{
    public class GetEnemyMoveJob : Job<Direction>
    {
        protected override async Task<Direction> Update()
        {
            Position enemyPos = Query.Single<CurrentAgentAspect>().AgentAspect.PhysBodyAspect.Position;

            return Physics.GetFreeDirectionsAround(enemyPos.Coord).GetRandom();
        }
    }
}