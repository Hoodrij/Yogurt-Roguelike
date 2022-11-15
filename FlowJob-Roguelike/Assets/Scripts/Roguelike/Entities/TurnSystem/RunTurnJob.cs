using System.Threading.Tasks;
using Core.Tools;
using Entities.TurnSystem;
using UnityAsync;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async Task Update()
        {
            await this.WaitSeconds(0.5f);
            
            await new WaitForMoveDecisionJob().Run();
            await new MoveCurrentAgentJob().Run();

            bool isLevelOver = await new GameOverCheckJob().Run();
            if (!isLevelOver) await new RunTurnJob().Run();
        }
    }
}