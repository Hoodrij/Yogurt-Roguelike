using System;
using System.Linq;
using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async UniTask<Void> Update()
        {
            await this.WaitSecondsRealtime(GetDelay());
            await new GiveTurnToNextAgentJob().Run();
            await new MoveCurrentAgentJob().Run();

            bool isLevelOver = await new CheckGameOverJob().Run();
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