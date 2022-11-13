using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using Roguelike.Entities.Enemy;
using Roguelike.Jobs;
using UnityEngine;

namespace Entities.TurnSystem
{
    public class WaitForMoveDecisionJob : Job
    {
        protected override async Task Update()
        {
            ActorAspect actorAspect = Aspect<CurrentActorAspect>.Single().ActorAspect;
            Vector2Int moveDecision = Vector2Int.zero;

            if (actorAspect.Has<Player>())
            {
                moveDecision = await new GetPlayerInputJob().Run();
            }
            else if (actorAspect.Has<Ai>())
            {
                moveDecision = Direction.Random;
            }
            
            actorAspect.Actor.MoveDecision = moveDecision;
        }
    }
}