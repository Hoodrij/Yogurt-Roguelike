using System.Collections.Generic;
using System.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Abilities
{
    public class AttackAbility : Ability
    {
        protected override async Task<AbilityOutcome> Run(Args args)
        {
            AbilityOutcome outcome = AbilityOutcome.ProceedTurn;
            
            IEnumerable<Entity> targets = Physics.GetEntitiesAtPosition(args.TargetPosition);
            foreach (Entity target in targets)
            {
                bool success = await new ChangeHealthJob().Run((target, -1));
                if (success)
                    outcome = AbilityOutcome.CompleteTurn;
            }

            return outcome;
        }
    }
}