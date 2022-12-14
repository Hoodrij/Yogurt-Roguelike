using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Entities
{
    public class AnimationJob : Job<Void, (Entity entity, AgentView.Animation animation)>
    {
        protected override async Task<Void> Run((Entity entity, AgentView.Animation animation) args)
        {
            if (!args.entity.TryGet(out AgentView view)) return default;
            
            await view.RunAnimation(args.animation);

            return default;
        }
    }
}