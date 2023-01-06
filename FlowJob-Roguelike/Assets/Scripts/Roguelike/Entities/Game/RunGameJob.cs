using System;
using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class RunGameJob : Job
    {
        public override async void Run()
        {
            await new GameFactoryJob().Run(); 

            while (!IsGameOver())
            {
                await new LevelFactoryJob().Run();
                await new RunTurnsJob().Run();
                
                // small delay before restart
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

                Entity level = Query.Of<Level>().Single();
                level.Kill();
            }
        }

        private static bool IsGameOver() => new CheckGameOverJob().Run();
    }
}