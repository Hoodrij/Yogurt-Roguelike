using System.Threading.Tasks;
using Core.Tools;
using Roguelike.Abilities;
using UnityEngine;

namespace Roguelike.Entities
{
    public class ZombieTurnJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            Direction direction = await new GetClosestTargetDirectionJob().Run(agentAspect);
            if (direction == Direction.None)
            {
                direction = await new GetEnemyMoveDirectionJob().Run(agentAspect);
            }

            Vector2Int newPosition = agentAspect.PhysBodyAspect.Position.Value + direction;
            await new RunAbilitiesJob().Run((agentAspect, newPosition));
            
            return default;
        }
    }
}