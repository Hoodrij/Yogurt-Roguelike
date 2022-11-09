using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async Task Run()
        {
            World world = new World();

            Entity game = Entity.Create()
                .Add<Game>()
                .Add<Life>();

            await game.Get<Life>().QuitEvent;

            "World Dispose test".log();
            world.Dispose();
        }
    }
}