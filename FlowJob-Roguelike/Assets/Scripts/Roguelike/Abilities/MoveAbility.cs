using Cysharp.Threading.Tasks;

namespace Roguelike.Abilities
{
    public class MoveAbility : IAbility
    {
        public async UniTask<AbilityOutcome> Run(IAbility.Args args)
        {
            AgentAspect agentAspect = args.AgentAspect;
            
            agentAspect.PhysBodyAspect.Position.Value = args.TargetPosition;
            agentAspect.View.UpdateView(agentAspect);
            
            return AbilityOutcome.ProceedTurn;
        }
    }
}