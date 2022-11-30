﻿using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class RunGameJob : Job
    {
        protected override async Task Run()
        {
            Entity entity = Entity.Create()
                .Add<Game>()
                .Add<Life>()
                .Add<Data>()
                .Add<Assets>();

            entity.Add(new Health
            {
                Value = entity.Get<Data>().StartingPlayerHealth
            });

            while (true)
            {
                await new LevelFactoryJob().Run();
                await new RunTurnsJob().Run();

                Entity level = Query.Of<Level>().Single();
                level.Kill();
            }
        }
    }
}