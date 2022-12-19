using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using UnityEngine;

namespace Roguelike.Abilities
{
    public enum AbilityOutcome
    {
        ProceedTurn = 1,
        CompleteTurn = 2,
    }
    
    public abstract class Ability : Job<Task<AbilityOutcome>, Ability.Args>
    {
        public struct Args
        {
            public AgentAspect AgentAspect;
            public Vector2Int TargetPosition;
        }
        
        public static readonly List<Ability> PlayerAbilities = new()
        {
            new AttackAbility(),
            new InteractAbility(),
            new MoveAbility(),
        };

        public static readonly List<Ability> ZombieAbilities = new()
        {
            new AttackAbility(),
            new MoveAbility(),
        };
    }
}