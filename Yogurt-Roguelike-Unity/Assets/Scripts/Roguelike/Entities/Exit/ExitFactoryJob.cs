﻿using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;

namespace Yogurt.Roguelike
{
    public struct ExitFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Entity entity = Level.Create()
                .Add(new Exit())
                .Add(new Collider
                {
                    Layer = CollisionLayer.Interactable
                })
                .Add(new Position
                {
                    Value = data.ExitPosition
                });

            TileView view = await assets.Environment.Spawn();
            entity.AddForLife(view);
            view.SetPosition(entity.Get<Position>().Value);
            
            return entity;
        }
    }
}