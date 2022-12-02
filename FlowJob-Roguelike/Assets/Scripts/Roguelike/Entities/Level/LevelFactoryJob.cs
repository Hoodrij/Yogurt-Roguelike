using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using Roguelike.Entities;
using Roguelike.Entities.Food;

namespace Roguelike.Jobs
{
    public class LevelFactoryJob : Job
    {
        protected override async Task Run()
        {
            Entity.Create()
                .Add<Level>();

            await new EnvironmentFactoryJob().Run();
            await new ExitFactoryJob().Run();
            for (int i = 0; i < 4.RandomTo(); i++)
            {
                await new FoodFactoryJob().Run();
            }
            
            await new PlayerFactoryJob().Run();
            for (int i = 0; i < 3; i++)
            {
                await new EnemyFactoryJob().Run();
            }
        }
    }
}