using System.Threading.Tasks;
using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Roguelike.Jobs
{
    public class SpawnEnvironmentJob : Job
    {
        protected override async UniTask Run()
        {
            Data data = Aspect<GameAspect>.Single().Data;
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

        private async UniTask<Entity> SpawnFloor(Vector2Int coord)
        {
            return Entity.Create()
                .Add<Floor>()
                .Add(new Position
                {
                    Coord = coord
                });
        }

        private async UniTask<Entity> SpawnWall(Vector2Int coord)
        {
            return Entity.Create()
                .Add<Wall>()
                .Add<Collider>()
                .Add(new Position
                {
                    Coord = coord
                });
        }
    }
}