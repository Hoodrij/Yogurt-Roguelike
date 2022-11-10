using FlowJob;

namespace Roguelike.Entities
{
    public struct LevelAspect : Aspect<LevelAspect>
    {
        public Entity Entity { get; set; }

        public Board Board => this.Get<Board>();
    }
}