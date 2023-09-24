using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public struct GameFactoryJob
    {
        public async UniTask<GameAspect> Run()
        {
            Assets assets = new Assets();
            Data data = await assets.Data.Load();
            
            Health health = new Health
            {
                Value = data.StartingPlayerHealth,
                OnHealthChangedJob = new UpdateGameUIJob()
            };

            UI ui = await assets.UI.Spawn();
            ui.UpdateView(health.Value);
            
            Entity entity = Entity.Create()
                .Add(assets)
                .Add(data)
                .Add(health)
                .Add(ui);

            return entity.As<GameAspect>();
        }
    }
}