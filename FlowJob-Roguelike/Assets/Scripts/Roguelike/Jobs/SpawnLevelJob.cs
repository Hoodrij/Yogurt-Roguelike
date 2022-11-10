using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class SpawnLevelJob : Job
    {
        protected override async UniTask Run()
        {
            Data data = Aspect<GameAspect>.Single().Data;
            
            Board board = new()
            {
                Cells = new Entity[data.BoardSize.x, data.BoardSize.y]
            };

            Entity.Create()
                .Set(board);

            for (int x = 0; x < data.BoardSize.x; x++)
            {
                for (int y = 0; y < data.BoardSize.y; y++)
                {
                    Vector2Int coord = new Vector2Int(x, y);
                    board.Cells[x, y] = await new SpawnCellJob(coord).Run();
                }
            }
        }
    }
}