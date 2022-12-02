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

            await this.WaitUntil(() =>
            {
                Direction direction = ReadInput();
                Vector2Int newPlayerPosition = playerPosition.Coord + direction;
                IEnumerable<Collider> collidersPlayerIsMovingAt = Physics.GetColliderAtPosition(newPlayerPosition);
                
                return direction != Direction.None
                       && playerCollider.CanMoveAt(collidersPlayerIsMovingAt);

            });
            
            return ReadInput();
            
            Direction ReadInput()
            {
                int x = (int) Input.GetAxisRaw("Horizontal");
                int y = (int) Input.GetAxisRaw("Vertical");

                return new Direction(x, y);
            }
        }
    }
}