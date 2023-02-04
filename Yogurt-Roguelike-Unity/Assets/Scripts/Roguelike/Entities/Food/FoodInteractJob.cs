using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public struct FoodInteractJob : Interactable.IInteractJob
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