using System.Collections.Generic;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Yogurt.Roguelike
{
    public class GetClosestTargetDirectionJob
    {
        public Direction Run(AgentAspect agentAspect)
        {
            Direction direction = GetTargetsAround(agentAspect).GetRandom();
            return direction;
        }
        
        private static IEnumerable<Direction> GetTargetsAround(AgentAspect agentAspect)
        {
            foreach (AgentAspect target in Query.Of<AgentAspect>())
            {
                if (target.Agent.Team == agentAspect.Agent.Team) 
                    continue;

                if (TryGetDirection(target, out Direction direction))
                    yield return direction;
            }

            bool TryGetDirection(AgentAspect target, out Direction direction)
            {
                Vector2Int deltaPos = target.PhysBodyAspect.Position.Value - agentAspect.PhysBodyAspect.Position.Value;
                direction = new Direction(deltaPos);
                return deltaPos.IsNormalized();
            }
        }
    }
}