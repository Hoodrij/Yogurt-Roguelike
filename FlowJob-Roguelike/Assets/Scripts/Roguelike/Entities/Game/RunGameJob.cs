using System;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class RunGameJob : Job
    {
        public override async Task Run()
        {
            await new GameFactoryJob().Run(); 

            while (!await IsGameOver())
            {
                await new LevelFactoryJob().Run();
                await new RunTurnsJob().Run();
                
                // small delay before restart
                await Task.Delay(TimeSpan.FromSeconds(0.1f));

                Entity level = Query.Of<Level>().Single();
                level.Kill();
            }
        }

        private static async Task<bool> IsGameOver() => await new CheckGameOverJob().Run();
    }
}