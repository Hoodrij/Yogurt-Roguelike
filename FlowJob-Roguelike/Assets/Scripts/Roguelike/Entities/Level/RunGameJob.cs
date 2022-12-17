using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using UnityAsync;

namespace Roguelike
{
    public class RunGameJob : Job
    {
        protected override async Task Run()
        {
            await new GameFactoryJob().Run(); 

            while (!await IsGameOver())
            {
                await new LevelFactoryJob().Run();
                await new RunTurnsJob().Run();
                
                // small delay before restart
                await this.WaitSeconds(0.1f);

                Entity level = Query.Of<Level>().Single();
                level.Kill();
            }
        }

        private static async Task<bool> IsGameOver() => await new CheckGameOverJob().Run();
    }
}