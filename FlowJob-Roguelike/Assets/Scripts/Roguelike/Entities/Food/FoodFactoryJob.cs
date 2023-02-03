using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using Yogurt;
using UnityEngine;

namespace Roguelike
{
    public struct FoodFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            Vector2Int spawnPosition = GetSpawnPosition();
            
            FoodData foodData = data.Foods.GetRandom();
            TileView view = await assets.Environment.Spawn();
            view.SetPosition(spawnPosition);
            view.SetView(foodData.Sprite);

            Entity entity = Level.Create()
                .Add(new Food
                {
                    Value = foodData.Amount
                })
                .Add(new Position
                {
                    Value = spawnPosition
                })
                .Add(new Collider
                {
                    Layer = CollisionLayer.Interactable
                })
                .Add(new Interactable
                {
                    InteractionJob = new FoodInteractJob()
                })
                .AddForLife(view);

            return entity;
            
            Vector2Int GetSpawnPosition()
            {
                return Physics.GetFreeCoords(data.SpawnRange).GetRandom();
            }
        }
    }
}