using FlowJob;

namespace Roguelike.Entities
{
    public struct LevelAspect : Aspect<LevelAspect>
    {
        public Entity Entity { get; set; }

        public Level Level => this.Get<Level>();
        public Board Board => this.Get<Board>();
    }
    
    public class Level : IComponent { }
}