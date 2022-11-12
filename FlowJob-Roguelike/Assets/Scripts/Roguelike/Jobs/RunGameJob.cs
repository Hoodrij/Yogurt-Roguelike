using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async UniTask Run()
        {
            WorldDebug wd = new WorldDebug();

            // Entity.Create()
            //     .Add<Health>()
            //     .Add<Actor>()
            //     .Add<Position>()
            //     .Add<Player>();
            //
            // PlayerAspect playerAspect = Aspect<PlayerAspect>.Single();
            // playerAspect.ActorAspect.Position.Coord = Vector2Int.one;

            Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add<Data>()
                .Add<Assets>();
            
            await new SpawnLevelJob().Run();
            await new RunTurnJob().Run();

            // Entity level = Query.With<Level>().Single();
            // level.Kill();
        }
    }
}