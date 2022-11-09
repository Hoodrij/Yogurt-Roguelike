using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async UniTask Run()
        {
            World world = new World();

            Entity.Create()
                .Set(new Game(world))
                .Add<Life>();

            new DisposeGameJob().Run();
        }
    }
}