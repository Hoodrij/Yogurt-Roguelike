using FlowJob;

namespace Roguelike.Entities
{
    public struct LevelAspect : Aspect<LevelAspect>
    {
        public Entity Entity { get; set; }

        public Level Level => this.Get<Level>();
    }

    public class Level : IComponent
    {
        
    }
}