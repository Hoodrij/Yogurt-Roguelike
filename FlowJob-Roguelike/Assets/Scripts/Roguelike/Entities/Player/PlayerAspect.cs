using FlowJob;

namespace Roguelike.Entities
{
    public struct PlayerAspect : Aspect<PlayerAspect>
    {
        public Entity Entity { get; set; }

        public Player Player => this.Get<Player>();
        public AgentAspect AgentAspect => this.GetAspect<AgentAspect>();
    }

    public class Player : IComponent
    {
        
    }
    
    public class Health : IComponent
    {
        public int Value = 100;
    }
}