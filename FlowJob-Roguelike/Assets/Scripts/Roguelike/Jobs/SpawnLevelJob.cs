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
                .Add<Level>()
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
            
            await new SpawnPlayerJob().Run();
            await new SpawnExitJob().Run();
        }
    }
}