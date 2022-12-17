using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class Health : IComponent
    {
        public int Value = 100;
        public Job<Void, Entity> OnHealthChangedJob;
    }
}