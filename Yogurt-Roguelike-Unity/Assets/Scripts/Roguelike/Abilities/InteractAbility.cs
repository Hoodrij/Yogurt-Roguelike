using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Abilities
{
    public class InteractAbility : IAbility
    {
        public async UniTask<AbilityOutcome> Run(IAbility.Args args)
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