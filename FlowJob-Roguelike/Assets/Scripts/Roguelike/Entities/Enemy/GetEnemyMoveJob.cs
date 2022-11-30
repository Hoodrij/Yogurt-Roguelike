using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;

namespace Roguelike.Entities
{
    public class GetEnemyMoveJob : Job<Direction>
    {
        protected override async Task<Direction> Run()
        {
            Position enemyPos = Query.Single<CurrentAgentAspect>().AgentAspect.PhysBodyAspect.Position;

            IEnumerable<Direction> freeDirectionsAround = Physics.GetFreeDirectionsAround(enemyPos.Coord);
            if (freeDirectionsAround.IsEmpty())
                return Direction.Random;
            return freeDirectionsAround.GetRandom();
        }
    }
}