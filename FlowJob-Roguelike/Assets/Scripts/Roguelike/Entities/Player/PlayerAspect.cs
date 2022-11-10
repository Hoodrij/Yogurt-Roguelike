using FlowJob;

namespace Roguelike.Entities
{
    public struct PlayerAspect : Aspect<PlayerAspect>
    {
        public Entity Entity { get; set; }

        public Player Player => this.Get<Player>();
        public Health Health => this.Get<Health>();
        public Actor Actor => this.Get<Actor>();
    }

    public class Player : IComponent
    {
        
    }
    
    public class Health : IComponent
    {
        public int Value;
    }

    public class Actor : IComponent
    {
        
    }
}