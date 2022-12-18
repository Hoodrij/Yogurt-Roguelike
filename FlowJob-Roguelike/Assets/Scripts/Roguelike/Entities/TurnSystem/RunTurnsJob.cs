using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class RunTurnsJob : Job
    {
        public override async Task Run()
        {
            while (!await IsExitReached() && !await IsGameOver())
            {
                await Task.Delay(TimeSpan.FromSeconds(GetDelay()));
                await new GiveTurnToNextAgentJob().Run();
                await new MakeTurnJob().Run();
            }
        }

        private async Task<bool> IsExitReached() => await new CheckExitReachedJob().Run();

        private async Task<bool> IsGameOver() => await new CheckGameOverJob().Run();

        private float GetDelay()
        {
            int agentsCount = Query.Of<Agent>().Count();
            return 0.15f / agentsCount;
        }
    }
}