using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public struct LevelFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Data data = Query.Single<Data>();
            
            Entity entity = Entity.Create()
                .Add(new Level());

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
            
            return entity;
        }
    }
}