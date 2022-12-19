using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class FoodInteractJob : Job<Task, (Entity food, Entity agent)>
    {
        public override async Task Run((Entity food, Entity agent) args)
        {
            Entity food = args.food;
            Entity agent = args.agent;

            if (!agent.TryGet(out Health health)) return;

            health.Value += food.Get<Food>().Value;
            
            food.Kill();
        }
    }
}