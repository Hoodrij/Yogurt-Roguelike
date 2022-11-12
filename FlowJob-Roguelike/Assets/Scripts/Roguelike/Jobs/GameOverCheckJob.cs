using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GameOverCheckJob : Job<bool>
    {
        protected override async UniTask<bool> Run()
        {
            PlayerAspect playerAspect = Aspect<PlayerAspect>.Single();
            bool isPlayerAlive = playerAspect.Health.Value > 0;

            Entity exit = Query.With<Exit>().With<Position>().Single();
            Vector2Int exitPos = exit.Get<Position>().Coord;
            bool isPlayerAtExit = false;
            return isPlayerAlive;
        }
    }
}