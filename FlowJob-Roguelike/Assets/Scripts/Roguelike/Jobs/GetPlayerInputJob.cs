using Core.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GetPlayerInputJob : Job<Vector2Int>
    {
        protected override async UniTask<Vector2Int> Run()
        {
            return Vector2Int.up;
        }
    }
}