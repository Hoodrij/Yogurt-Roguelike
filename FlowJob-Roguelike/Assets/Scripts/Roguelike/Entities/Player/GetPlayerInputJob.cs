using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class GetPlayerInputJob : Job<Direction>
    {
        protected override async Task<Direction> Run()
        {
            AgentAspect agentAspect = Query.Single<PlayerAspect>().AgentAspect;
            PhysBodyAspect bodyAspect = agentAspect.PhysBodyAspect;
            Collider playerCollider = bodyAspect.Collider;
            Position playerPosition = bodyAspect.Position;

            Direction direction = default;

            await this.WaitUntil(() =>
            {
                direction = ReadInput();
                return DirectionIsValid() && CanMoveAtDirection();
            });

            return direction;
            
            
            Direction ReadInput()
            {
                int x = (int) Input.GetAxisRaw("Horizontal");
                int y = (int) Input.GetAxisRaw("Vertical");

                return new Direction(x, y);
            }

            bool DirectionIsValid()
            {
                return direction != Direction.None;
            }

            bool CanMoveAtDirection()
            {
                Vector2Int newPlayerPosition = playerPosition.Value + direction;
                return Physics.CanMoveAtPoint(newPlayerPosition, playerCollider.CanMoveAt);
            }
        }
    }
}