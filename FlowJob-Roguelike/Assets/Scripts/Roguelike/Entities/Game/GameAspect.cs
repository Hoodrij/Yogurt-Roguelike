using FlowJob;

namespace Roguelike
{
    public struct GameAspect : Aspect<GameAspect>
    {
        public Entity Entity { get; set; }

        public Data Data => this.Get<Data>();
        public Assets Assets => this.Get<Assets>();

        public Health Health => this.Get<Health>();
    }
}