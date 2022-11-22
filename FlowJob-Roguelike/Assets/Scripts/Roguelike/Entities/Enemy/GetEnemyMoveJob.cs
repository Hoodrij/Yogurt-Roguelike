using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class GetEnemyMoveJob : Job<Direction>
    {
        protected override async Task<Direction> Update()
        {
            Position enemyPos = Query.Single<CurrentAgentAspect>().AgentAspect.Position;
            int triesCount = 10;
            for (int i = 0; i < triesCount; i++)
            {
                Direction direction = Direction.Random;
                Vector2Int newPos = enemyPos.Coord + direction;
                if (Collider.GetColliderAtPosition(newPos) == null)
                {
                    return direction;
                }
            }
            
            return Direction.None;
        }
    }
}