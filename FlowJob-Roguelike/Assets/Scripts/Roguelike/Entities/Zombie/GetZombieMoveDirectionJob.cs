using System.Collections.Generic;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Roguelike
{
    public class GetZombieMoveDirectionJob : Job<Direction, AgentAspect>
    {
        public override Direction Run(AgentAspect agentAspect)
        {
            return GetFreeDirections(agentAspect.PhysBodyAspect)
                .GetRandom();
        }
        
        private static IEnumerable<Direction> GetFreeDirections(PhysBodyAspect body)
        {
            foreach (Direction direction in Direction.All)
            {
                Vector2Int newPoint = body.Position.Value + direction;
                if (Physics.CanMoveAt(newPoint, body))
                {
                    yield return direction;
                }
            }
        }
    }
}