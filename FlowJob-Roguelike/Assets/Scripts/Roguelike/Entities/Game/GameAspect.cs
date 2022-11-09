using FlowJob;

namespace Roguelike
{
    public struct GameAspect : Aspect<GameAspect>
    {
        public Entity Entity { get; set; }

        public Game Game => this.Get<Game>();
        public Life Life => this.Get<Life>();
    }
}