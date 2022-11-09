using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async UniTask Run()
        {
            Entity.Create()
                .Add<Game>()
                .Add<Life>();

            new SpawnLevelJob().Run();
        }
    }
}