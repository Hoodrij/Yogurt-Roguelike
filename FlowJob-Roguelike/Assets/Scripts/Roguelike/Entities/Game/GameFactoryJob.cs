using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class GameFactoryJob : Job<GameAspect>
    {
        protected override async Task<GameAspect> Run()
        {
            Assets assets = new Assets();
            Data data = await assets.Data.Load();

            Entity entity = Entity.Create()
                .Add(assets)
                .Add(data);

            Health health = new Health
            {
                Value = data.StartingPlayerHealth
            };
            entity.Add(health);

            UI ui = await assets.UI.Spawn();
            ui.UpdateView(health.Value);
            entity.Add(ui);

            return entity.ToAspect<GameAspect>();
        }
    }
}