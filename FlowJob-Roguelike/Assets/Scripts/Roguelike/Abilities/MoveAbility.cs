using System.Threading.Tasks;

namespace Roguelike.Abilities
{
    public class MoveAbility : Ability
    {
        protected override async Task<AbilityOutcome> Run(Args args)
        {
            AgentAspect agentAspect = args.AgentAspect;
            
            agentAspect.PhysBodyAspect.Position.Value = args.TargetPosition;
            agentAspect.View.UpdateView(agentAspect);
            
            return AbilityOutcome.ProceedTurn;
        }
    }
}