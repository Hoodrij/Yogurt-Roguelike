using Entities.Environment;
using FlowJob;
using Tools;

namespace Roguelike.Entities
{
    public class Assets : IComponent
    {
        public readonly SO<Data> Data = new("Data"); 

        public readonly Asset<AgentView> Player = new("Player");
        public readonly Asset<AgentView> Enemy = new("Enemy");
        public readonly Asset<TileView> Tile = new("Tile");
        public readonly Asset<TileView> Interactable = new("Interactable");
        
        public readonly Asset<UI> UI = new("UI");
    }
}