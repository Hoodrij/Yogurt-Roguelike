﻿using Core.Tools;
using FlowJob;

namespace Roguelike.Entities
{
    public class Agent : IComponent
    {
        public Team Team;
        public Job<Void, AgentAspect> TurnJob { get; set; }
    }

    public struct AgentAspect : Aspect<AgentAspect>
    {
        public Entity Entity { get; set; }
        
        public Agent Agent => this.Get<Agent>();
        public Health Health => this.Get<Health>();
        public AgentView View => this.Get<AgentView>();
        public PhysBodyAspect PhysBodyAspect => this.GetAspect<PhysBodyAspect>();
    }
}