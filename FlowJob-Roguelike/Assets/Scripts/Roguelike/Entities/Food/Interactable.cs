using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class Interactable : IComponent
    {
        public Job<Task, (Entity interactable, Entity agent)> InteractionJob;
    }
}