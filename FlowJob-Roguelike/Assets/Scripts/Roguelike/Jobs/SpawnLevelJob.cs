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
                Cells = new Cell[data.BoardSize.x, data.BoardSize.y]
            };

            Entity.Create()
                .Set(board);

            for (int x = 0; x < data.BoardSize.x; x++)
            {
                for (int y = 0; y < data.BoardSize.y; y++)
                {
                    board.Cells[x, y] = new Cell
                    {
                        Coord = new Vector2Int(x, y)
                    };
                }
            }
            
            Vector2Int playerCoord = Vector2Int.one;
            Entity playerEntity = await new SpawnPlayerJob(playerCoord).Run();
            board.SetCell(playerCoord, playerEntity);

            Vector2Int exitCoord = data.BoardSize - Vector2Int.one - Vector2Int.one;
            Entity exitEntity = await new SpawnExitJob(exitCoord).Run();
            board.SetCell(exitCoord, exitEntity);

            
        }
    }
}