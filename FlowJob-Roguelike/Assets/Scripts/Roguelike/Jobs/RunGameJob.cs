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
            WorldDebug wd = new WorldDebug();

            Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add<Data>()
                .Add<Assets>();
            
            new SpawnLevelJob().Run();
            
            // Entity level = Query.With<Level>().Single();
            // level.Kill();
        }
    }
}