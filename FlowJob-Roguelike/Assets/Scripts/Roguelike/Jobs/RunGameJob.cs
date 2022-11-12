using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async Task Run()
        {
            WorldDebug wd = new WorldDebug();

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