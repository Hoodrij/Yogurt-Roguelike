using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
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

                Entity level = Query.Of<Level>().Single();
                level.Kill();
            }
        }

        private static async Task<bool> IsGameOver() => await new CheckGameOverJob().Run();
    }
}