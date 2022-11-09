using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike.Jobs
{
    public class DisposeGameJob : Job
    {
        protected override async UniTask Run()
        {
            GameAspect game = Aspect<GameAspect>.Single();
            await game.Life.QuitEvent;
            
            game.Game.World.Dispose();
        }
    }
}