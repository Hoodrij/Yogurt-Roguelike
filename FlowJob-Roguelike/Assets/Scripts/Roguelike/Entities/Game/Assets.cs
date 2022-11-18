using FlowJob;
using Tools;

namespace Roguelike.Entities
{
    public class Assets : IComponent
    {
        public readonly Asset<AgentView> Player = new("Player");
    }
}