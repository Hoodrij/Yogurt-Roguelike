using Yogurt;

namespace Roguelike
{
    public class Health : IComponent
    {
        public int Value;
        public IHealthChangedJob OnHealthChangedJob; 
        
        public interface IHealthChangedJob
        {
            void Run(Entity entity);
        }
    }
}