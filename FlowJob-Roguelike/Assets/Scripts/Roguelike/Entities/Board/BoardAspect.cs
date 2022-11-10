using FlowJob;

namespace Roguelike.Entities
{
    public struct BoardAspect : Aspect<BoardAspect>
    {
        public Entity Entity { get; set; }

        public Board Board => this.Get<Board>();
    }
}