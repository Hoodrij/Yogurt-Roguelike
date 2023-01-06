using Core.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Roguelike
{
    public class GetPlayerInputJob : Job<UniTask<Direction>, AgentAspect>
    {
        public override async UniTask<Direction> Run(AgentAspect player)
        {
            Position playerPosition = player.PhysBodyAspect.Position;

            Direction direction;

            bool isInputValid;
            do
            {
                direction = ReadInput();
                isInputValid = DirectionIsValid() && CanMoveAtDirection();
                await UniTask.Yield();
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
                return Physics.CanMoveAt(newPlayerPosition, player.PhysBodyAspect);
            }
        }
    }
}