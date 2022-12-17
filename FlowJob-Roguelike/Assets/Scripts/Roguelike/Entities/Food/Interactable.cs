using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class Interactable : IComponent
    {
        public Job<Void, (Entity interactable, Entity agent)> InteractionJob;
    }
}