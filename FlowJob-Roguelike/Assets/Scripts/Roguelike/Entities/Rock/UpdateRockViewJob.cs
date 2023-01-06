using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class UpdateRockViewJob : Job<Void, Entity>
    {
        public override Void Run(Entity entity)
        {
            TileView tileView = entity.Get<TileView>();
            Health health = entity.Get<Health>();
            RockData rockData = entity.Get<RockData>();

            tileView.SetView(rockData.GetSprite(health));
            
            return default;
        }
    }
}