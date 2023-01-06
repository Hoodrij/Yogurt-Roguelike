using System;
using System.Linq;
using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class RunTurnsJob : Job<UniTask>
    {
        public override async UniTask Run()
        {
            while (!IsExitReached() && !IsGameOver())
            {
                await UniTask.Delay(TimeSpan.FromSeconds(GetDelay()));
                new GiveTurnToNextAgentJob().Run();
                await new MakeTurnJob().Run();
            }
        }

        private bool IsExitReached() => new CheckExitReachedJob().Run();

        private bool IsGameOver() => new CheckGameOverJob().Run();

        private float GetDelay()
        {
            int agentsCount = Query.Of<Agent>().Count();
            return 0.15f / agentsCount;
        }
    }
}