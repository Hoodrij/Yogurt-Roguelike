using FlowJob;
using Tools;

namespace Roguelike.Entities
{
    public class Assets : IComponent
    {
        public readonly Asset<PlayerView> Player = new("Player");
    }
}