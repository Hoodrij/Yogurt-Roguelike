using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class GameFactoryJob : Job<GameAspect>
    {
        public override async Task<GameAspect> Run()
        {
            Assets assets = new Assets();
            Data data = await assets.Data.Load();
            
            Health health = new Health
            {
                Value = data.StartingPlayerHealth
            };

            UI ui = await assets.UI.Spawn();
            ui.UpdateView(health.Value);
            
            Entity entity = Entity.Create()
                .Add(assets)
                .Add(data)
                .Add(health)
                .Add(ui);

            return entity.ToAspect<GameAspect>();
        }
    }
}