using System.Threading.Tasks;
using Core.Tools;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GetPlayerInputJob : Job<Vector2Int>
    {
        protected override async Task<Vector2Int> Update()
        {
            int x = (int) Input.GetAxisRaw("Horizontal");
            int y = (int) Input.GetAxisRaw("Vertical");
            
            return new(x, y);
        }
    }
}