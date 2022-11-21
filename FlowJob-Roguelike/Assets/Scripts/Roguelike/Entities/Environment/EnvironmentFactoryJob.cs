using System.Threading.Tasks;
using Core.Tools;
using Entities.Environment;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class EnvironmentFactoryJob : Job
    {
        protected override async Task<Void> Update()
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
            
            foreach (Entity entity in Query.Of<TileView>().With<Position>())
            {
                entity.Get<TileView>().UpdateView(entity.Get<Position>());
            }

            return default;
        }

        private async Task<Entity> SpawnFloor(Vector2Int coord)
        {
            Assets assets = Query.Single<Assets>();

            Entity entity = Level.Create()
                .Add<Floor>()
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(await assets.Floor.Spawn());

            return entity;
        }

        private async Task<Entity> SpawnWall(Vector2Int coord)
        {
            Assets assets = Query.Single<Assets>();

            Entity entity = Level.Create()
                .Add<Wall>()
                .Add<Collider>()
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(await assets.Wall.Spawn());

            return entity;
        }
    }
}