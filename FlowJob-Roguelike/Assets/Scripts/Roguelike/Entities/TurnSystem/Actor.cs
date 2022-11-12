using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Actor : IComponent
    {
        public Vector2Int MoveDecision; 
    }
    
    public class CurrentTurnActor : IComponent { }

    public struct ActorAspect : Aspect<ActorAspect>
    {
        public Entity Entity { get; set; }
        
        public Actor Actor => this.Get<Actor>();
        public Position Position => this.Get<Position>();
    }

    public struct CurrentActorAspect : Aspect<CurrentActorAspect>
    {
        public Entity Entity { get; set; }

        public ActorAspect ActorAspect => this.GetAspect<ActorAspect>();
        public CurrentTurnActor Tag => this.Get<CurrentTurnActor>();
    }
}