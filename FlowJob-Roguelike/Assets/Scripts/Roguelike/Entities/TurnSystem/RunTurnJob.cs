using System.Threading.Tasks;
using Core.Tools;
using UnityAsync;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async Task<Void> Update()
        {
            await this.WaitSeconds(0.2f);

            await new GiveTurnToNextAgentJob().Run();
            await new MoveCurrentAgentJob().Run();

            bool isLevelOver = await new GameOverCheckJob().Run();
            if (!isLevelOver)
            {
                
                await this.Run();
            }
            
            return default;
        }
    }
}