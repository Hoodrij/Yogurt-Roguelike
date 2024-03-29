﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Abilities;

namespace Yogurt.Roguelike
{
    public class Agent : IComponent
    {
        public Team Team;
        public ITurnJob TurnJob;
        public List<IAbility> Abilities = new();
        
        public interface ITurnJob
        {
            UniTask Run(AgentAspect agentAspect);
        }
    }

    public struct AgentAspect : IAspect
    {
        public Entity Entity { get; set; }
        
        public Agent Agent => this.Get<Agent>();
        public Health Health => this.Get<Health>();
        public AgentView View => this.Get<AgentView>();
        public PhysBodyAspect PhysBodyAspect => this.Get<PhysBodyAspect>();
    }
}