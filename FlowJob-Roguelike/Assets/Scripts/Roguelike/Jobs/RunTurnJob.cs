using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async UniTask Run()
        {
            await this.WaitSeconds(1);
            
            Vector2Int playerInput = await new GetPlayerInputJob().Run();

            CurrentActorAspect currentActorAspect = Aspect<CurrentActorAspect>.Single();
            Actor actor = currentActorAspect.ActorAspect.Actor;
            actor.MoveDecision = playerInput;

            await new MoveActorJob(currentActorAspect.ActorAspect).Run();            

            bool isLevelOver = await new GameOverCheckJob().Run();
            if (!isLevelOver) await new RunTurnJob().Run();
        }
    }
}