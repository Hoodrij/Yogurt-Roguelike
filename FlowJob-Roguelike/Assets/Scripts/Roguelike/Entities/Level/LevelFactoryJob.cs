using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class LevelFactoryJob : Job
    {
        protected override async Task Update()
        {
            Entity.Create()
                .Add<Level>();

            // await new SpawnEnvironmentJob().Run();
            await new ExitFactoryJob().Run();
            await new PlayerFactoryJob().Run();
        }
    }
}