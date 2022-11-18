using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class Agent : IComponent
    {
        public Job<Direction> MoveJob { get; set; }
    }
    
    public class CurrentTurnAgent : IComponent { }

    public struct AgentAspect : Aspect<AgentAspect>
    {
        public Entity Entity { get; set; }
        
        public Agent Agent => this.Get<Agent>();
        public Position Position => this.Get<Position>();
        public AgentView View => this.Get<AgentView>();
    }

    public struct CurrentAgentAspect : Aspect<CurrentAgentAspect>
    {
        public Entity Entity { get; set; }

        public AgentAspect AgentAspect => this.GetAspect<AgentAspect>();
        public CurrentTurnAgent Tag => this.Get<CurrentTurnAgent>();
    }
}