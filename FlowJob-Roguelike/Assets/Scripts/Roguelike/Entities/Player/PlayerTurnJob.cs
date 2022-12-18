using System.Threading.Tasks;
using Core.Tools;
using Roguelike.Abilities;
using UnityEngine;

namespace Roguelike
{
    public class PlayerTurnJob : Job<Void, AgentAspect>
    {
        public override async Task<Void> Run(AgentAspect player)
        {
            Direction direction = await new GetPlayerInputJob().Run(player);
            Vector2Int newPosition = player.PhysBodyAspect.Position.Value + direction;
            await new RunAbilitiesJob().Run((player, newPosition));

            await new ChangeHealthJob().Run((player.Entity, -1));

            return default;
        }
    }
}