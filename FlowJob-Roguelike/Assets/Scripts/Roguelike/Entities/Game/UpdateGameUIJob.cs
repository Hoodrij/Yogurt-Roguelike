using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Entities.Player
{
    public class UpdateGameUIJob : Job<Void, Entity>
    {
        protected override async Task<Void> Run(Entity entity)
        {
            GameAspect gameAspect = Query.Single<GameAspect>();
            Health health = entity.Get<Health>();
            gameAspect.Get<UI>().UpdateView(health.Value);

            return default;
        }
    }
}