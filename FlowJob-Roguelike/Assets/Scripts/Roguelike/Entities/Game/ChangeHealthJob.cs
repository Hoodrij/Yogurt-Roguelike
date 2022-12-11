using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class ChangeHealthJob : Job<bool, (Entity target, int delta)>
    {
        protected override async Task<bool> Run((Entity target, int delta) args)
        {
            Entity target = args.target;
            if (!target.TryGet(out Health health)) return false;
            
            health.Value += args.delta;

            if (target.Has<Player>())
            {
                GameAspect gameAspect = Query.Single<GameAspect>();
                gameAspect.Get<UI>().UpdateView(health.Value);
            }
            
            if (health.Value <= 0)
            {
                target.Kill();
            }
            
            return true;
        }
    }
}