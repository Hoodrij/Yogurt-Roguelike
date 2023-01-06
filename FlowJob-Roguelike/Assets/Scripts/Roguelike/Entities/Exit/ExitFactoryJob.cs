using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class ExitFactoryJob : Job<UniTask<Entity>>
    {
        public override async UniTask<Entity> Run()
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