using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class UpdateGameUIJob : Job<Void, Entity>
    {
        public override async Task<Void> Run(Entity entity)
        {
            GameAspect gameAspect = Query.Single<GameAspect>();
            Health health = entity.Get<Health>();
            gameAspect.Get<UI>().UpdateView(health.Value);

            return default;
        }
    }
}