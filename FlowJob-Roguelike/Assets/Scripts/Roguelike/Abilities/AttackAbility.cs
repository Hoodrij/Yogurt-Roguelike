using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike.Abilities
{
    public class AttackAbility : Ability
    {
        public override async UniTask<AbilityOutcome> Run(Args args)
        {
            bool damageDealt = false;
            
            IEnumerable<Entity> targets = Physics.GetEntitiesAtPosition(args.TargetPosition);
            foreach (Entity target in targets)
            {
                new AnimationJob().Run((target, AgentView.Animation.Hit));
                
                damageDealt = await new ChangeHealthJob().Run((target, -1));
            }

            if (damageDealt)
            {
                await new AnimationJob().Run((args.AgentAspect.Entity, AgentView.Animation.Attack));
            }

            return damageDealt ? AbilityOutcome.CompleteTurn : AbilityOutcome.ProceedTurn;
        }
    }
}