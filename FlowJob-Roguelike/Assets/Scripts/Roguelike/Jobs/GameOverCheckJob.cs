using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GameOverCheckJob : Job<bool>
    {
        protected override async Task<bool> Run()
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