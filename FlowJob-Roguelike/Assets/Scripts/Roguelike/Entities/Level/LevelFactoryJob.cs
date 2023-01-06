using Core.Tools;
using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class LevelFactoryJob : Job<UniTask>
    {
        public override async UniTask Run()
        {
            Data data = Query.Single<Data>();
            
            Entity.Create()
                .Add<Level>();

            await new EnvironmentFactoryJob().Run();
            await new ExitFactoryJob().Run();
            await new PlayerFactoryJob().Run();
            
            for (int i = 0; i < data.RocksCount.RandomTo(); i++)
            {
                await new RockFactoryJob().Run();
            }
            
            for (int i = 0; i < data.FoodCount.RandomTo(); i++)
            {
                await new FoodFactoryJob().Run();
            }
            
            for (int i = 0; i < data.EnemiesCount.RandomTo(); i++)
            {
                await new ZombieFactoryJob().Run();
            }
        }
    }
}