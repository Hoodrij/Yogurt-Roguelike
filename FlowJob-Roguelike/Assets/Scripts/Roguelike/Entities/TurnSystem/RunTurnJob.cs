using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async Task Update()
        {
            await this.WaitSeconds(0.5f);
            
            Vector2Int playerInput = await new GetPlayerInputJob().Run();

            CurrentActorAspect currentActorAspect = Aspect<CurrentActorAspect>.Single();
            Actor actor = currentActorAspect.ActorAspect.Actor;
            actor.MoveDecision = playerInput;

            await new MoveActorJob(currentActorAspect.ActorAspect).Run();

            // await this.Run<RunTurnJob>();

            bool isLevelOver = await new GameOverCheckJob().Run();
            if (!isLevelOver) await new RunTurnJob().Run();
        }
    }
}