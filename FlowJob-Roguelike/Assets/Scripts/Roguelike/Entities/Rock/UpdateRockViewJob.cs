using Yogurt;

namespace Roguelike
{
    public struct UpdateRockViewJob : Health.IHealthChangedJob
    {
        public void Run(Entity entity)
        {
            TileView tileView = entity.Get<TileView>();
            Health health = entity.Get<Health>();
            RockData rockData = entity.Get<RockData>();

            tileView.SetView(rockData.GetSprite(health));
        }
    }
}