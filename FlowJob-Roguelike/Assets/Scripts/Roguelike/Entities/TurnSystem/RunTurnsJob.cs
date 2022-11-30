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
            while (!await IsGameOver())
            {
                await this.WaitSecondsRealtime(GetDelay());
                await new GiveTurnToNextAgentJob().Run();
                await new MoveCurrentAgentJob().Run();
            }
        }

        private async Task<bool> IsGameOver()
        {
            return await new CheckGameOverJob().Run();
        }

        private float GetDelay()
        {
            int agentsCount = Query.Of<Agent>().Count();

            return 0.15f / agentsCount;
        }
    }
}