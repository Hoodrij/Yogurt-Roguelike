using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class AnimationJob : Job<UniTask, (Entity entity, AgentView.Animation animation)>
    {
        public override async UniTask Run((Entity entity, AgentView.Animation animation) args)
        {
            if (!args.entity.TryGet(out AgentView view)) return;
            
            await view.RunAnimation(args.animation);
        }
    }
}