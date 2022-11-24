using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async Task<Void> Update()
        {
            Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add<Data>()
                .Add<Assets>();
            
            await new LevelFactoryJob().Run();
            await new RunTurnJob().Run();

            Entity level = Query.Of<Level>().Single();
            level.Kill();

            return default;
        }
    }
}