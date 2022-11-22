using Entities.Environment;
using FlowJob;
using Tools;

namespace Roguelike.Entities
{
    public class Assets : IComponent
    {
        public readonly Asset<AgentView> Player = new("Player");
        public readonly Asset<AgentView> Enemy = new("Enemy");
        public readonly Asset<TileView> Wall = new("Wall");
        public readonly Asset<TileView> Floor = new("Floor");
        public readonly Asset<TileView> Exit = new("Exit");
    }
}