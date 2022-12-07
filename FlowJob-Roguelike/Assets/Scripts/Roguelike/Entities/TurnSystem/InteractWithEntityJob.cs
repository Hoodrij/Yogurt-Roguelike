using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using Roguelike.Entities.Food;

namespace Entities.TurnSystem
{
    public class InteractWithEntityJob : Job<bool, (AgentAspect agent, Entity target)>
    {
        protected override async Task<bool> Run((AgentAspect agent, Entity target) data)
        {
            Entity target = data.target;
            
            if (target.TryGet(out Food food))
            {
                await new ChangeHealthJob().Run(food.Value);
                target.Kill();
                return true;
            }

            if (target.Has<Player>())
            {
                await new ChangeHealthJob().Run(-1);
                return false;
            }
            
            if (target.TryGet(out Health health))
            {
                health.Value--;
                if (health.Value <= 0)
                {
                    target.Kill();
                }
                return false;
            }
            
            return true;
        }
    }
}