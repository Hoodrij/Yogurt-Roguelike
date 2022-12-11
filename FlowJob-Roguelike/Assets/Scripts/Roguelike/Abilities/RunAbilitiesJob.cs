using System.Threading.Tasks;
using Core.Tools;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class RunAbilitiesJob : Job<Void, (AgentAspect agentAspect, Vector2Int targetPosition)>
    {
        protected override async Task<Void> Run((AgentAspect agentAspect, Vector2Int targetPosition) args)
        {
            AgentAspect agentAspect = args.agentAspect;
            Vector2Int newPosition = args.targetPosition;
            
            foreach (Ability agentAbility in agentAspect.Agent.Abilities)
            {
                AbilityOutcome abilityOutcome = await agentAbility.Run(new Ability.Args
                {
                    AgentAspect = agentAspect,
                    TargetPosition = newPosition
                });
                if (abilityOutcome == AbilityOutcome.CompletingTurn)
                    break;
            }

            return default;
        }
    }
}