using System.Threading.Tasks;
using Core.Tools;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async Task<Void> Update()
        {
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