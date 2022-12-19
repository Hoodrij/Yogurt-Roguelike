using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class AnimationJob : Job<Task, (Entity entity, AgentView.Animation animation)>
    {
        public override async Task Run((Entity entity, AgentView.Animation animation) args)
        {
            if (!args.entity.TryGet(out AgentView view)) return;
            
            await view.RunAnimation(args.animation);
        }
    }
}