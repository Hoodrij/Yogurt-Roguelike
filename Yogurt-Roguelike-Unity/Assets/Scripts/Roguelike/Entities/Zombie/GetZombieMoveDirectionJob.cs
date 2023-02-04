using System.Collections.Generic;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Yogurt.Roguelike
{
    public class GetZombieMoveDirectionJob
    {
        public Direction Run(AgentAspect agentAspect)
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