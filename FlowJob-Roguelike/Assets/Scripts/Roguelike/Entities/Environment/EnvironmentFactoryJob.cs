using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using Entities.Environment;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class EnvironmentFactoryJob : Job
    {
        protected override async Task Run()
        {
            Data data = Query.Single<Data>();
            int xSize = data.BoardSize.x;
            int ySize = data.BoardSize.y;

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x == 0 || x == xSize - 1 
                        || y == 0 || y == ySize - 1)
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

        private async Task<Entity> SpawnFloor(Vector2Int coord)
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            TileView tileView = await assets.Tile.Spawn();
            tileView.SetView(data.FloorSprites.GetRandom());
            tileView.SetPosition(coord);
            
            Entity entity = Level.Create()
                .Add<Floor>()
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(tileView);

            return entity;
        }

        private async Task<Entity> SpawnWall(Vector2Int coord)
        {
            Data data = Query.Single<Data>();
            Assets assets = Query.Single<Assets>();

            TileView tileView = await assets.Tile.Spawn();
            tileView.SetView(data.WallSprites.GetRandom());
            tileView.SetPosition(coord);
            
            Entity entity = Level.Create()
                .Add<Wall>()
                .Add(Collider.Hard)
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(tileView);

            return entity;
        }
    }
}