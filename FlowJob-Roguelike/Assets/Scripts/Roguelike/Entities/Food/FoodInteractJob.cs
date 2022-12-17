using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class FoodInteractJob : Job<Void, (Entity food, Entity agent)>
    {
        protected override async Task<Void> Run((Entity food, Entity agent) args)
        {
            Entity food = args.food;
            Entity agent = args.agent;

            if (!agent.TryGet(out Health health)) return default;

            health.Value += food.Get<Food>().Value;
            
            food.Kill();

            return default;
        }
    }
}