using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class Health : IComponent
    {
        public int Value;
        public Job<Void, Entity> OnHealthChangedJob; 
    }
}