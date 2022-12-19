using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class RunTurnsJob : Job<Task>
    {
        public override async Task Run()
        {
            while (!IsExitReached() && !IsGameOver())
            {
                await Task.Delay(TimeSpan.FromSeconds(GetDelay()));
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