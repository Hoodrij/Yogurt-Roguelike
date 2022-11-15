using Entities.TurnSystem;
using FlowJob;

namespace Roguelike.Entities
{
    public class Agent : IComponent
    {
        public Direction MoveDecision; 
    }
    
    public class CurrentTurnAgent : IComponent { }

    public struct AgentAspect : Aspect<AgentAspect>
    {
        public Entity Entity { get; set; }
        
        public Agent Agent => this.Get<Agent>();
        public Position Position => this.Get<Position>();

        public AgentType Type => Entity.Has<Player>() ? AgentType.Player : AgentType.Ai;
    }

    public struct CurrentAgentAspect : Aspect<CurrentAgentAspect>
    {
        public Entity Entity { get; set; }

        public AgentAspect AgentAspect => this.GetAspect<AgentAspect>();
        public CurrentTurnAgent Tag => this.Get<CurrentTurnAgent>();
    }
}