using System.Threading.Tasks;
using Core.Tools;
using Entities.TurnSystem;
using Roguelike;
using Roguelike.Abilities;
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
            Vector2Int newPosition = agentAspect.PhysBodyAspect.Position.Value + direction;
            await new RunAbilitiesJob().Run((agentAspect, newPosition));

            await new ChangeHealthJob().Run((agentAspect.Entity, -1));

            return default;
        }
    }
}