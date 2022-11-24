using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;
using Physics = Entities.Physics;

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

                return new Direction(x, y);
            }

            Position playerPosition = Query.Single<PlayerAspect>().AgentAspect.PhysBodyAspect.Position;
            Collider colliderPlayerIsMovingAt;

            await this.WaitUntil(() =>
            {
                Direction direction = ReadInput();
                Vector2Int newPlayerPosition = playerPosition.Coord + direction;
                colliderPlayerIsMovingAt = Physics.GetColliderAtPosition(newPlayerPosition);
                bool canMoveAtCollider = colliderPlayerIsMovingAt == null || colliderPlayerIsMovingAt.IsTrigger;
                
                return direction != Direction.None
                       && canMoveAtCollider;
            });

            return ReadInput();
        }
    }
}