﻿using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class ExitFactoryJob : Job<Task<Entity>>
    {
        public override async Task<Entity> Run()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Entity entity = Level.Create()
                .Add<Exit>()
                .Add(new Collider
                {
                    Layer = CollisionLayer.Interactable
                })
                .Add(new Position
                {
                    Value = data.ExitPosition
                });

            TileView view = await assets.Environment.Spawn();
            entity.Add(view);
            view.SetPosition(entity.Get<Position>().Value);
            
            return entity;
        }
    }
}