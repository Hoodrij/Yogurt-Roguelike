using System.Threading.Tasks;
using Core.Tools;
using UnityAsync;

namespace Roguelike.Entities
{
    public class GetEnemyMoveJob : Job<Direction>
    {
        protected override async Task<Direction> Update()
        {
            await this.WaitSeconds(0.05f);
            return Direction.Random;
        }
    }
}