using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async UniTask Run()
        {
            WorldDebug worldDebug = new WorldDebug();
            
            Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add<Data>();

            new SpawnLevelJob().Run();

            Entity single = Query.With<Level>().Single();
            single.Kill();

            await this.WaitLateUpdate();
        }
    }
}