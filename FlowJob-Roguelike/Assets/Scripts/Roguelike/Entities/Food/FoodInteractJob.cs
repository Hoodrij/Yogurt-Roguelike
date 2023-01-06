using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class FoodInteractJob : Job<UniTask, (Entity food, Entity agent)>
    {
        public override async UniTask Run((Entity food, Entity agent) args)
        {
            Entity food = args.food;
            Entity agent = args.agent;

            if (!agent.TryGet(out Health health)) return;

            health.Value += food.Get<Food>().Value;
            
            food.Kill();
        }
    }
}