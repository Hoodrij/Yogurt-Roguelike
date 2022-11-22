using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;
using UnityEngine;
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

            await this.WaitWhile(() =>
            {
                Direction direction = ReadInput();
                Vector2Int newPlayerPosition = playerPosition.Coord + direction;
                return direction == Direction.None 
                       || Physics.GetColliderAtPosition(newPlayerPosition) != null;
            });

            return ReadInput();
        }
    }
}