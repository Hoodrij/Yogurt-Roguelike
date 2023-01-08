using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class ChangeHealthJob
    {
        public async UniTask<bool> Run((Entity target, int delta) args)
        {
            Entity target = args.target;
            if (!target.TryGet(out Health health)) return false;
            
            health.Value += args.delta;
            health.OnHealthChangedJob?.Run(target);
            
            if (health.Value <= 0)
            {
                target.Kill();
            }
            
            return true;
        }
    }
}