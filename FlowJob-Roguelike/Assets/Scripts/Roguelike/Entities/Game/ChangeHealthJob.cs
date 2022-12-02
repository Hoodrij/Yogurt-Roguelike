using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class ChangeHealthJob : Job<Void, int>
    {
        protected override async Task<Void> Run(int delta)
        {
            GameAspect gameAspect = Query.Single<GameAspect>();
            
            Health playerHealth = gameAspect.Health;
            playerHealth.Value += delta;
            
            gameAspect.Get<UI>().UpdateView(playerHealth.Value);
            
            return default;
        }
    }
}