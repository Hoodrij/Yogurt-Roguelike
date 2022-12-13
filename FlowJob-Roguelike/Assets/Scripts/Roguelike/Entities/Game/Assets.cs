using Entities.Environment;
using FlowJob;
using Tools;

namespace Roguelike.Entities
{
    public class Assets : IComponent
    {
        public readonly SO<Data> Data = new("Data"); 

        public readonly Asset<AgentView> Player = new("Entities/Player/Player");
        public readonly Asset<AgentView> Enemy = new("Entities/Zombie/Zombie");
        public readonly Asset<TileView> Tile = new("Entities/Tile");
        public readonly Asset<TileView> Interactable = new("Entities/Interactable");
        
        public readonly Asset<UI> UI = new("UI");
    }
}