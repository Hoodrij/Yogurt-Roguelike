using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class CheckGameOverJob : Job<bool>
    {
        protected override async Task<bool> Update()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<Player>().Single();

            Vector2Int playerPosition = agentAspect.PhysBodyAspect.Position.Coord;
            IEnumerable<Entity> entitiesAtPosition = Physics.GetEntitiesAtPosition(playerPosition);
            if (entitiesAtPosition.Any(entity => entity.Has<Exit>()))
                return true;

            if (agentAspect.Health.Value <= 0)
                return true;

            return false;
        }
    }
}