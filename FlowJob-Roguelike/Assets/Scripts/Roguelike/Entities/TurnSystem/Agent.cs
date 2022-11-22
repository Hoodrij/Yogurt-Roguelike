using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class Agent : IComponent
    {
        public Job<Direction> MoveJob { get; set; }
    }
    
    public class CurrentAgentTag : IComponent { }

    public struct AgentAspect : Aspect<AgentAspect>
    {
        public Entity Entity { get; set; }
        
        public Agent Agent => this.Get<Agent>();
        public Health Health => this.Get<Health>();
        public AgentView View => this.Get<AgentView>();
        public PhysBodyAspect PhysBodyAspect => this.GetAspect<PhysBodyAspect>();
    }

    public struct CurrentAgentAspect : Aspect<CurrentAgentAspect>
    {
        public Entity Entity { get; set; }

        public AgentAspect AgentAspect => this.GetAspect<AgentAspect>();
        public CurrentAgentTag Tag => this.Get<CurrentAgentTag>();
    }
}