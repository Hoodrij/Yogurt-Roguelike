using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public struct RunTurnsJob
    {
        public async UniTask Run()
        {
            while (!IsExitReached() && !IsGameOver())
            {
                await UniTask.Delay(GetDelay());
                new GiveTurnToNextAgentJob().Run();
                await new MakeTurnJob().Run();
            }
            
            
            bool IsExitReached() => new CheckExitReachedJob().Run();
            bool IsGameOver() => new CheckGameOverJob().Run();
            TimeSpan GetDelay()
            {
                float delay = Query.Single<Data>().TurnDelay;
                int agentsCount = Query.Of<Agent>().Count();
                return TimeSpan.FromSeconds(delay / agentsCount);
            }
        }
    }
}