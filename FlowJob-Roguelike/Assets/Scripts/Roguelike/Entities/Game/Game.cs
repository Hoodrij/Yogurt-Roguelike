using FlowJob;

namespace Roguelike
{
    public class Game : IComponent
    {
        public World World;

        public Game(World world)
        {
            World = world;
        }
    }
}