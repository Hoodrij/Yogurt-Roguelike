using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class UpdateRockViewJob : Job<Void, Entity>
    {
        protected override async Task<Void> Run(Entity entity)
        {
            TileView tileView = entity.Get<TileView>();
            Health health = entity.Get<Health>();
            RockData rockData = entity.Get<RockData>();

            tileView.SetView(rockData.GetSprite(health));
            return default;
        }
    }
}