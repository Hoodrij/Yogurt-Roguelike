using System.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using Roguelike.Entities.Food;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class InteractAbility : Ability
    {
        protected override async Task<AbilityOutcome> Run(Args args)
        {
            AgentAspect agentAspect = args.AgentAspect;
            Vector2Int targetPosition = args.TargetPosition;
            
            foreach (Entity target in Physics.GetEntitiesAtPosition(targetPosition))
            {
                if (!target.TryGet(out Interactable interactable)) continue;
                
                await interactable.InteractionJob.Run((target, agentAspect.Entity));
                target.Kill();
            }

            return AbilityOutcome.ProceedTurn;
        }
    }
}