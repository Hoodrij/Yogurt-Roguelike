using System.Threading.Tasks;
using Core.Tools;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class RunAbilitiesJob : Job<Task, (AgentAspect agentAspect, Vector2Int targetPosition)>
    {
        public override async Task Run((AgentAspect agentAspect, Vector2Int targetPosition) args)
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
                if (abilityOutcome == AbilityOutcome.CompleteTurn)
                    break;
            }
        }
    }
}