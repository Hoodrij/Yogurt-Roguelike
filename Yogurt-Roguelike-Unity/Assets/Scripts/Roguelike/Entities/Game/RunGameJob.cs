using System;
using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public struct RunGameJob
    {
        public async void Run()
        {
            await new GameFactoryJob().Run();

            while (!IsGameOver())
            {
                Entity level = await new LevelFactoryJob().Run();
                await new RunTurnsJob().Run();
                
                // small delay before restart
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                level.Kill();
            }
            return;

            bool IsGameOver() => new CheckGameOverJob().Run();
        }
    }
}