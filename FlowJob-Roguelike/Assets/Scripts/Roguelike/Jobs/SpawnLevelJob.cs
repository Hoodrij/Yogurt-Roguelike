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
            Data data = Aspect<GameAspect>.Single().Data;

            Entity.Create()
                .Add<Level>();

            await new SpawnEnvironmentJob().Run();
            await new SpawnPlayerJob().Run();
            await new SpawnExitJob().Run();
        }
    }
}