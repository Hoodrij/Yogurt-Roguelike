using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class GameFactoryJob : Job<GameAspect>
    {
        protected override async Task<GameAspect> Run()
        {
            Data data = new Data();
            Assets assets = new Assets();

            Entity entity = Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add(assets)
                .Add(data);

            Health health = new Health
            {
                Value = data.StartingPlayerHealth
            };
            entity.Add(health);

            UI ui = await assets.UI.Spawn();
            entity.Add(ui);
            ui.UpdateView(health.Value);

            return entity.ToAspect<GameAspect>();
        }
    }
}