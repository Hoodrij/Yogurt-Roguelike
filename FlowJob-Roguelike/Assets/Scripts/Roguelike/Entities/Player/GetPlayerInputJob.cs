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

            Health playerHealth = Query.Single<PlayerAspect>().AgentAspect.Health;
            playerHealth.Value--.log();
            
            return ReadInput();
        }
    }
}