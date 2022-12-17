using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    public class GetClosestTargetDirectionJob : Job<Direction, AgentAspect>
    {
        protected override async Task<Direction> Run(AgentAspect agentAspect)
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