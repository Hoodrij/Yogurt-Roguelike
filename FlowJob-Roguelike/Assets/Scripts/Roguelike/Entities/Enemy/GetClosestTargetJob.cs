using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class GetClosestTargetJob : Job<Entity, AgentAspect>
    {
        protected override async Task<Entity> Run(AgentAspect agentAspect)
        {
            return GetTargetsAround(agentAspect).GetRandom();
        }
        
        private static IEnumerable<Entity> GetTargetsAround(AgentAspect agentAspect)
        {
            foreach (AgentAspect target in Query.Of<AgentAspect>())
            {
                if (target.Agent.Team == agentAspect.Agent.Team) continue;

                if (IsCloseToTarget(target))
                    yield return target.Entity;
            }

            bool IsCloseToTarget(AgentAspect target)
            {
                Vector2Int deltaPos = agentAspect.PhysBodyAspect.Position.Value - target.PhysBodyAspect.Position.Value;
                return new Direction(deltaPos).IsValid();
            }
        }
    }
}