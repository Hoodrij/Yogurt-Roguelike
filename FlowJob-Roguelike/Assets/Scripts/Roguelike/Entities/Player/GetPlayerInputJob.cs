using System.Threading.Tasks;
using Core.Tools;
using UnityAsync;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GetPlayerInputJob : Job<Direction>
    {
        protected override async Task<Direction> Update()
        {
            Direction ReadInput()
            {
                int x = (int) Input.GetAxisRaw("Horizontal");
                int y = (int) Input.GetAxisRaw("Vertical");

                return new Vector2Int(x, y);
            }

            await this.WaitWhile(() => ReadInput() == Direction.None);
            
            return ReadInput();
        }
    }
}