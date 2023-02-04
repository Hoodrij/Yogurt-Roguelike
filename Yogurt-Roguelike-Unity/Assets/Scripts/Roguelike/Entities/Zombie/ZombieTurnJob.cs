using Cysharp.Threading.Tasks;
using Roguelike.Abilities;
using UnityEngine;

namespace Roguelike
{
    public class ZombieTurnJob : Agent.ITurnJob
    {
        public async UniTask Run(AgentAspect agentAspect)
        {
            Direction direction = new GetClosestTargetDirectionJob().Run(agentAspect);
            if (direction == Direction.None)
            {
                direction = new GetZombieMoveDirectionJob().Run(agentAspect);
            }

            Vector2Int newPosition = agentAspect.PhysBodyAspect.Position.Value + direction;
            await new RunAbilitiesJob().Run((agentAspect, newPosition));
        }
    }
}