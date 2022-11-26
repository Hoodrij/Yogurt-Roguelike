using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async UniTask<Void> Update()
        {
            Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add<Data>()
                .Add<Assets>();
            
            while (true)
            {
                await new LevelFactoryJob().Run();
                await new RunTurnJob().Run();
            
                Entity level = Query.Of<Level>().Single();
                level.Kill();
            }
            
            return default;
        }
    }
}