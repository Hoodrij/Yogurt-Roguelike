using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GameOverCheckJob : Job<bool>
    {
        protected override async Task<bool> Update()
        {
            AgentAspect playerAspect = Query.Of<AgentAspect>().With<Player>().Single();

            Vector2Int playerPosition = playerAspect.PhysBodyAspect.Position.Coord;
            IEnumerable<Entity> entitiesAtPosition = Physics.GetEntitiesAtPosition(playerPosition);
            if (entitiesAtPosition.Any(entity => entity.Has<Exit>()))
                return true;

            // bool isPlayerAlive = playerAspect.AgentAspect.Health.Value > 0;

            // playerAspect.AgentAspect.Position.Coord.log();
            
            // Physics.GetColliderAtPosition()
            // playerAspect.PhysBodyAspect.Collider

            // Entity exit = Query.With<Exit>().With<Position>().Single();
            // Vector2Int exitPos = exit.Get<Position>().Coord;
            // bool isPlayerAtExit = false;

            return false;
        }
    }
}