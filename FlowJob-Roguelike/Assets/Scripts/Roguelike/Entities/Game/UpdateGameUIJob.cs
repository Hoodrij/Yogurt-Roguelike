using FlowJob;

namespace Roguelike
{
    public class UpdateGameUIJob : Health.IHealthChangedJob
    {
        public void Run(Entity entity)
        {
            GameAspect gameAspect = Query.Single<GameAspect>();
            Health health = entity.Get<Health>();
            gameAspect.Get<UI>().UpdateView(health.Value);
        }
    }
}