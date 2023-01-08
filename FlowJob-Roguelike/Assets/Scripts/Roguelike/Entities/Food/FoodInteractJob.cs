using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class FoodInteractJob : Interactable.IInteractJob
    {
        public async UniTask Run((Entity interactable, Entity agent) args)
        {
            Entity food = args.interactable;
            Entity agent = args.agent;

            if (!agent.TryGet(out Health health)) return;

            health.Value += food.Get<Food>().Value;
            
            food.Kill();
        }
    }
}