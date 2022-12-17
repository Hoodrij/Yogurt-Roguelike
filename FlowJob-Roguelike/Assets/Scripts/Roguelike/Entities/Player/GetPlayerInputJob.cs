using System.Threading.Tasks;
using Core.Tools;
using UnityAsync;
using UnityEngine;

namespace Roguelike
{
    public class GetPlayerInputJob : Job<Direction, AgentAspect>
    {
        protected override async Task<Direction> Run(AgentAspect agentAspect)
        {
            Position playerPosition = agentAspect.PhysBodyAspect.Position;

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
                return Physics.CanMoveAt(newPlayerPosition, agentAspect.PhysBodyAspect);
            }
        }
    }
}