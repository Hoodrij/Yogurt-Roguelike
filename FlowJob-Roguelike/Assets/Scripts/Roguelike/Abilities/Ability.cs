using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Roguelike.Abilities
{
    public enum AbilityOutcome
    {
        ProceedTurn = 1,
        CompleteTurn = 2,
    }
    
    public interface IAbility
    {
        UniTask<AbilityOutcome> Run(Args args);
        
        public struct Args
        {
            public AgentAspect AgentAspect;
            public Vector2Int TargetPosition;
        }
        
        public static readonly List<IAbility> PlayerAbilities = new()
        {
            new AttackAbility(),
            new InteractAbility(),
            new MoveAbility(),
        };

        public static readonly List<IAbility> ZombieAbilities = new()
        {
            new AttackAbility(),
            new MoveAbility(),
        };
    }
}