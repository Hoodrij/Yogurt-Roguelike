using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class Interactable : IComponent
    {
        public Job<UniTask, (Entity interactable, Entity agent)> InteractionJob;
    }
}