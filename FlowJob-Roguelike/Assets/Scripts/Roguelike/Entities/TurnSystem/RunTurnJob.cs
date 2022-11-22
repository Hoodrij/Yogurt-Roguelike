using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async Task<Void> Update()
        {
            await this.WaitSecondsRealtime(GetDelay());
            await new GiveTurnToNextAgentJob().Run();
            await new MoveCurrentAgentJob().Run();

            bool isLevelOver = await new GameOverCheckJob().Run();
            if (!isLevelOver)
            {
                await this.Run();
            }
            
            return default;
        }

        private float GetDelay()
        {
            int agentsCount = Query.Of<Agent>().Count();

            return 0.15f / agentsCount;
        }
    }
}