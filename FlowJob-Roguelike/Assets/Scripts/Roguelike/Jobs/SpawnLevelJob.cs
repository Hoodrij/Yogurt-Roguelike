using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class SpawnLevelJob : Job
    {
        protected override async UniTask Run()
        {
            Entity.Create()
                .Add<Level>();

            await new SpawnEnvironmentJob().Run();
            await new SpawnExitJob().Run();
            await new SpawnPlayerJob().Run();
        }
    }
}