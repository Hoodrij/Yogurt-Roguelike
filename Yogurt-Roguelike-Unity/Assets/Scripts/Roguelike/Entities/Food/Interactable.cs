using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public class Interactable : IComponent
    {
        public IInteractJob InteractionJob;
        
        public interface IInteractJob
        {
            UniTask Run((Entity interactable, Entity agent) args);
        }
    }
}