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

            Entity game = Entity.Create().Add<Game>();
            Entity level = Entity.Create().Add<Level>();
            
            level.SetParent(game);
            
            game.Kill();
            Query.With<Game>().Single();

            Entity data = Entity.Create().Add<Data>();

            // Entity.Create()
            //     .Add<Game>()
            //     .Add<Life>()
            //     .Add<Data>();
            //
            // new SpawnLevelJob().Run();
            //
            // Entity level = Query.With<Level>().Single();
            // level.Kill();
        }
    }
}