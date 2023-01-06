using Core.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class RunAbilitiesJob : Job<UniTask, (AgentAspect agentAspect, Vector2Int targetPosition)>
    {
        public override async UniTask Run((AgentAspect agentAspect, Vector2Int targetPosition) args)
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