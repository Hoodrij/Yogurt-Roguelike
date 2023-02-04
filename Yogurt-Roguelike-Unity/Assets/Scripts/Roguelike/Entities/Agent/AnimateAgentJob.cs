using Cysharp.Threading.Tasks;
using Yogurt;

namespace Roguelike
{
    public class AnimateAgentJob
    {
        public async UniTask Run((Entity entity, AgentView.Animation animation) args)
        {
            if (!args.entity.TryGet(out AgentView view)) return;
            
            await view.RunAnimation(args.animation);
        }
    }
}