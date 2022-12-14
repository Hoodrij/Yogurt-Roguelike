using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;

namespace Roguelike.Abilities
{
    public class AttackAbility : Ability
    {
        protected override async Task<AbilityOutcome> Run(Args args)
        {
            bool damageDealt = false;
            
            IEnumerable<Entity> targets = Physics.GetEntitiesAtPosition(args.TargetPosition);
            foreach (Entity target in targets)
            {
                await new AnimationJob().Run((target, AgentView.Animation.Hit));
                
                damageDealt = await new ChangeHealthJob().Run((target, -1));
            }

            if (damageDealt)
            {
                await new AnimationJob().Run((args.AgentAspect.Entity, AgentView.Animation.Attack));
            }

            await Wait.Seconds(0.1f);

            return damageDealt ? AbilityOutcome.CompleteTurn : AbilityOutcome.ProceedTurn;
        }
    }
}