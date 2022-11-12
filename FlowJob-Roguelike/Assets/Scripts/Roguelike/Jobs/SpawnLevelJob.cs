using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class SpawnLevelJob : Job
    {
        protected override async Task Run()
        {
            Entity.Create()
                .Add<Level>();

            // await new SpawnEnvironmentJob().Run();
            await new SpawnExitJob().Run();
            await new SpawnPlayerJob().Run();
        }
    }
}