using System.Threading.Tasks;
using Core.Tools;
using Entities.Environment;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class ExitFactoryJob : Job<Entity>
    {
        protected override async Task<Entity> Run()
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
                    Coord = data.ExitPosition
                });

            TileView view = await assets.Exit.Spawn();
            entity.Add(view);
            view.SetPosition(entity.Get<Position>().Coord);
            
            return entity;
        }
    }
}