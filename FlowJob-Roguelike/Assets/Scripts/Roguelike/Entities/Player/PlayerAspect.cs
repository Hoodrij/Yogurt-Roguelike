using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public struct PlayerAspect : Aspect<PlayerAspect>
    {
        public Entity Entity { get; set; }

        public Player Player => this.Get<Player>();
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
        public Vector2Int Coord;
    }
}