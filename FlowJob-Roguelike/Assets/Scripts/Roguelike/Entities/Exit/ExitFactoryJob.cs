using System.Threading.Tasks;
using Core.Tools;
using Entities.Environment;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class ExitFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Update()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Vector2Int coord = data.BoardSize - Vector2Int.one - Vector2Int.one;
            Entity entity = Level.Create()
                .Add<Exit>()
                .Add(new Collider { IsTrigger = true })
                .Add(new Position
                {
                    Coord = coord
                });

            TileView view = await assets.Exit.Spawn();
            entity.Add(view);
            view.UpdateView(entity.Get<Position>());
            
            return entity;
        }
    }
}