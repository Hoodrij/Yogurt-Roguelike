using System.Threading.Tasks;
using Core.Tools;
using UnityEngine;

namespace Roguelike
{
    public class GetPlayerInputJob : Job<Direction, AgentAspect>
    {
        public override async Task<Direction> Run(AgentAspect agentAspect)
        {
            Position playerPosition = agentAspect.PhysBodyAspect.Position;

            Direction direction;

            bool isInputValid;
            do
            {
                direction = ReadInput();
                isInputValid = DirectionIsValid() && CanMoveAtDirection();
                await Task.Yield();
            } while (!isInputValid);

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