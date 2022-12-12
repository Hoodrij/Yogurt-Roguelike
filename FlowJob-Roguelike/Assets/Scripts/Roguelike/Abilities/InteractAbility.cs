using System.Threading.Tasks;

namespace Roguelike.Abilities
{
    public class InteractAbility : Ability
    {
        protected override async Task<AbilityOutcome> Run(Args args)
        {
            return AbilityOutcome.ProceedTurn;
        }
    }
}