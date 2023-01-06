using System.Collections.Generic;
using System.Linq;
using Core.Tools;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    public class CheckExitReachedJob : Job<bool>
    {
        public override bool Run()
        {
            if (!Query.Single<PlayerAspect>().Exist())
                return false;

            AgentAspect playerAgent = Query.Single<PlayerAspect>().AgentAspect;

            Vector2Int playerPosition = playerAgent.PhysBodyAspect.Position.Value;
            IEnumerable<Entity> entitiesAtPosition = Physics.GetEntitiesAtPosition(playerPosition);
            if (entitiesAtPosition.Any(entity => entity.Has<Exit>()))
                return true;

            return false;
        }
    }
}