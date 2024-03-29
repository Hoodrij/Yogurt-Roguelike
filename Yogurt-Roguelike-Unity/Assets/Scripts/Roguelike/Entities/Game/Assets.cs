﻿using Yogurt.Roguelike.Tools;

namespace Yogurt.Roguelike
{
    public class Assets : IComponent
    {
        public readonly SO<Data> Data = new("Data"); 

        public readonly Asset<AgentView> Player = new("Entities/Player/Player");
        public readonly Asset<AgentView> Zombie = new("Entities/Zombie/Zombie");
        public readonly Asset<TileView> Background = new("Entities/Background");
        public readonly Asset<TileView> Environment = new("Entities/Environment");
        
        public readonly Asset<UI> UI = new("UI");
    }
}