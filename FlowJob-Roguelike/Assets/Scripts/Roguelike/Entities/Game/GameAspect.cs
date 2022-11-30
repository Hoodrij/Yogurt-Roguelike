using FlowJob;

namespace Roguelike.Entities
{
    public struct GameAspect : Aspect<GameAspect>
    {
        public Entity Entity { get; set; }

        public Game Game => this.Get<Game>();
        public Life Life => this.Get<Life>();
        public Data Data => this.Get<Data>();
        public Assets Assets => this.Get<Assets>();

        public Health Health => this.Get<Health>();
    }
}