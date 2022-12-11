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
            return AbilityOutcome.ProceedingTurn;
        }
    }
}