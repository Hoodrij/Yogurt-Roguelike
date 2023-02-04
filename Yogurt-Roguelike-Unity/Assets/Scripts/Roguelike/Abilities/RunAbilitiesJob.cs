using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class RunAbilitiesJob
    {
        public async UniTask Run((AgentAspect agentAspect, Vector2Int targetPosition) args)
        {
            AgentAspect agentAspect = args.agentAspect;
            Vector2Int newPosition = args.targetPosition; 
            
            foreach (IAbility agentAbility in agentAspect.Agent.Abilities)
            {
                AbilityOutcome abilityOutcome = await agentAbility.Run(new IAbility.Args
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