using System.Threading.Tasks;
using Core.Tools;
using Entities.TurnSystem;
using Roguelike;
using Roguelike.Entities;
using Roguelike.Jobs;
using UnityEngine;

namespace Entities
{
    public class PlayerTurnJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            Direction direction = await new GetPlayerInputJob().Run(agentAspect);
            await new ChangeGameHealthJob().Run(-1);
            
            await new AgentMoveJob().Run((agentAspect, direction));
            // bool shouldMoveAtPosition = true;
            
            // Vector2Int newPos = agentAspect.PhysBodyAspect.Position.Value + direction;
            // foreach (Entity target in Physics.GetEntitiesAtPosition(newPos).Except(agentAspect.Entity).ToList())
            // {
            //     shouldMoveAtPosition &= await new InteractWithEntityJob().Run((agentAspect, target));
            // }
            
            // if (shouldMoveAtPosition)
            // {
            // }

            return default;
        }
    }
}