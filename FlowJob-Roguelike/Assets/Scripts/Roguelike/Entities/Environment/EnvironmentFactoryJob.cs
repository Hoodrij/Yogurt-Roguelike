using Core.Tools;
using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    public class EnvironmentFactoryJob : Job<UniTask>
    {
        public override async UniTask Run()
        {
            Data data = Query.Single<Data>();
            int xSize = data.BoardSize.x;
            int ySize = data.BoardSize.y;

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    bool isFramePosition = x == 0 || x == xSize - 1 
                                        || y == 0 || y == ySize - 1;
                    if (isFramePosition)
                    {
                        await SpawnWall(new Vector2Int(x, y));
                    }
                    else
                    {
                        await SpawnFloor(new Vector2Int(x, y));
                    }
                }
            }
        }

        private async UniTask<Entity> SpawnFloor(Vector2Int coord)
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            TileView tileView = await assets.Background.Spawn();
            tileView.SetView(data.FloorSprites.GetRandom());
            tileView.SetPosition(coord);
            
            Entity entity = Level.Create()
                .Add(new Position
                {
                    Value = coord
                })
                .Add(tileView); 

            return entity;
        }

        private async UniTask<Entity> SpawnWall(Vector2Int coord)
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            TileView tileView = await assets.Environment.Spawn();
            tileView.SetView(data.WallSprites.GetRandom());
            tileView.SetPosition(coord);
            
            Entity entity = Level.Create()
                .Add(Collider.Hard)
                .Add(new Position
                {
                    Value = coord
                })
                .Add(tileView);

            return entity;
        }
    }
}