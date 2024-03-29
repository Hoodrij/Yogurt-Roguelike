﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using Yogurt.Roguelike.Abilities;

namespace Yogurt.Roguelike
{
    public class PlayerTurnJob : Agent.ITurnJob
    {
        public async UniTask Run(AgentAspect player)
        {
            Direction direction = await new GetPlayerInputJob().Run(player);
            Vector2Int newPosition = player.PhysBodyAspect.Position.Value + direction;
            await new RunAbilitiesJob().Run((player, newPosition));

            await new ChangeHealthJob().Run((player.Entity, -1));
        }
    }
}