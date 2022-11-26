using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class LevelFactoryJob : Job
    {
        protected override async UniTask<Void> Update()
        {
            Entity.Create()
                .Add<Level>();

            await new EnvironmentFactoryJob().Run();
            await new ExitFactoryJob().Run();
            await new PlayerFactoryJob().Run();
            for (int i = 0; i < 4; i++)
            {
                await new EnemyFactoryJob().Run();
            }

            return default;
        }
    }
}