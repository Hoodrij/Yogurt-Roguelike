using System.Collections.Generic;
using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GetPlayerInputJob : Job<Direction>
    {
        protected override async UniTask<Direction> Update()
        {
            Direction ReadInput()
            {
                int x = (int) Input.GetAxisRaw("Horizontal");
                int y = (int) Input.GetAxisRaw("Vertical");

                return new Direction(x, y);
            }

            PhysBodyAspect bodyAspect = Query.Single<CurrentAgentAspect>().AgentAspect.PhysBodyAspect;
            Collider playerCollider = bodyAspect.Collider;
            Position playerPosition = bodyAspect.Position;

            await this.WaitUntil(() =>
            {
                Direction direction = ReadInput();
                Vector2Int newPlayerPosition = playerPosition.Coord + direction;
                IEnumerable<Collider> collidersPlayerIsMovingAt = Physics.GetColliderAtPosition(newPlayerPosition);
                
                return direction != Direction.None
                       && playerCollider.CanMoveAt(collidersPlayerIsMovingAt);
            });

            return ReadInput();
        }
    }
}