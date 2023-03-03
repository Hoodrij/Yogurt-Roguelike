using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike
{
    public struct RockFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Vector2Int spawnPosition = GetSpawnPosition();
            if (spawnPosition == default) return Entity.Null;

            RockData rockData = data.Rocks.GetRandom();

            Health health = new()
            {
                Value = 2,
                OnHealthChangedJob = new UpdateRockViewJob()
            };
            
            TileView view = await assets.Environment.Spawn();
            view.SetPosition(spawnPosition);
            view.SetView(rockData.GetSprite(health));

            Entity entity = Level.Create()
                .Add(rockData)
                .Add(health)
                .AddForLife(view)
                .Add(new Position
                {
                    Value = spawnPosition
                })
                .Add(new Collider
                {
                    Layer = CollisionLayer.Destructible
                });

            return entity;
            
            Vector2Int GetSpawnPosition()
            {
                return Physics.GetFreeCoords(data.SpawnRange).GetRandom();
            }
        }
    }
}