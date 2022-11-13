using System.Threading.Tasks;
using Core.Tools;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GetPlayerInputJob : Job<Vector2Int>
    {
        protected override async Task<Vector2Int> Update()
        {
            return Vector2Int.up;
        }
    }
}