using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class CheckExitReachedJob : Job<bool>
    {
        protected override async Task<bool> Run()
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