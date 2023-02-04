using Cysharp.Threading.Tasks;
using Yogurt;

namespace Roguelike
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