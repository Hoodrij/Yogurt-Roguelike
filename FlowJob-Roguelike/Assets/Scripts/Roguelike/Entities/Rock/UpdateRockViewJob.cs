using System.Threading.Tasks;
using Core.Tools;
using Entities.Environment;
using FlowJob;

namespace Roguelike.Entities
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