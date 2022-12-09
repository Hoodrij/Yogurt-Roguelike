using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class GetEnemyMoveDirectionJob : Job<Direction, AgentAspect>
    {
        protected override async Task<Direction> Run(AgentAspect agentAspect)
        {
            Vector2Int origin = agentAspect.PhysBodyAspect.Position.Value;
            return GetFreeDirections(origin)
                .GetRandom();
        }
        
        private static IEnumerable<Direction> GetFreeDirections(Vector2Int origin)
        {
            foreach (Direction direction in Direction.All)
            {
                Vector2Int newPoint = origin + direction;
                if (Physics.GetEntitiesAtPosition(newPoint).IsEmpty())
                {
                    yield return direction;
                }
            }
        }
    }
}