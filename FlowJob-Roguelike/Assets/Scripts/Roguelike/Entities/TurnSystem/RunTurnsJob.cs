using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;

namespace Roguelike.Jobs
{
    public class RunTurnsJob : Job
    {
        protected override async Task Run()
        {
            while (!await IsExitReached() && !await IsGameOver())
            {
                await this.WaitSecondsRealtime(GetDelay());
                await new GiveTurnToNextAgentJob().Run();
                await new MoveTurnOwnerJob().Run();
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