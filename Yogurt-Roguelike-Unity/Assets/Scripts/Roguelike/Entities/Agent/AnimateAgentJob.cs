using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
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