using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class RunTurnsJob
    {
        public async UniTask Run()
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
            float delay = Query.Single<Data>().TurnDelay;
            int agentsCount = Query.Of<Agent>().Count;
            return delay / agentsCount;
        }
    }
}