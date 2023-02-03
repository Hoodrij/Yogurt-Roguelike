using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Yogurt;

namespace Roguelike.Abilities
{
    public class AttackAbility : IAbility
    {
        public async UniTask<AbilityOutcome> Run(IAbility.Args args)
        {
            bool damageDealt = false;
            
            IEnumerable<Entity> targets = Physics.GetEntitiesAtPosition(args.TargetPosition);
            foreach (Entity target in targets)
            {
                new AnimateAgentJob().Run((target, AgentView.Animation.Hit));
                
                damageDealt = await new ChangeHealthJob().Run((target, -1));
            }

            if (damageDealt)
            {
                await new AnimateAgentJob().Run((args.AgentAspect.Entity, AgentView.Animation.Attack));
            }

            return damageDealt ? AbilityOutcome.CompleteTurn : AbilityOutcome.ProceedTurn;
        }
    }
}