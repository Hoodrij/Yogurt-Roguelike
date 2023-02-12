namespace Yogurt.Roguelike
{
    public struct PlayerAspect : IAspect
    {
        public Entity Entity { get; set; }

        public Player Player => this.Get<Player>();
        public AgentAspect AgentAspect => this.Get<AgentAspect>();
    }

    public class Player : IComponent
    {
        
    }
}