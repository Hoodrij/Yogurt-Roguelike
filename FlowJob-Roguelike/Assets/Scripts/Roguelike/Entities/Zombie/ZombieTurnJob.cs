using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Abilities;
using UnityEngine;

namespace Roguelike.Entities
{
    public class ZombieTurnJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            Direction direction = await new GetEnemyMoveDirectionJob().Run(agentAspect); 
            // Direction direction = await new GetZombieTurnDirection().Run(agentAspect);
            Vector2Int newPosition = agentAspect.PhysBodyAspect.Position.Value + direction;
            await new RunAbilitiesJob().Run((agentAspect, newPosition));
            
            // Entity target = await new GetClosestTargetJob().Run(agentAspect);
            // if (target.Exist)
            // {
                // await new AttackJob().Run((agentAspect, target));
                // return default;
            // }
            
            // Direction direction = await new GetEnemyMoveDirectionJob().Run(agentAspect);
            // await new AgentMoveJob().Run((agentAspect, direction));
            
            return default;
        }
    }
}