using Cysharp.Threading.Tasks;
using FlowJob;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class InteractAbility : Ability
    {
        public override async UniTask<AbilityOutcome> Run(Args args)
        {
            AgentAspect agentAspect = args.AgentAspect;
            Vector2Int targetPosition = args.TargetPosition;
            
            foreach (Entity target in Physics.GetEntitiesAtPosition(targetPosition))
            {
                if (!target.TryGet(out Interactable interactable)) continue;
                
                await interactable.InteractionJob.Run((target, agentAspect.Entity));
            }

            return AbilityOutcome.ProceedTurn;
        }
    }
}