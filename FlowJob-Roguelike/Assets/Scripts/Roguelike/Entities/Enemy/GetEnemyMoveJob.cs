using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class GetEnemyMoveJob : Job<Direction>
    {
        protected override async Task<Direction> Update()
        {
            Position enemyPos = Query.Single<CurrentAgentAspect>().AgentAspect.PhysBodyAspect.Position;

            return Collider.GetFreeCoordsAround(enemyPos.Coord).GetRandom();
        }
    }
}