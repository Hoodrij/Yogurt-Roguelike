﻿using System.Threading.Tasks;
using Core.Tools;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Entities;
using UnityAsync;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class RunTurnJob : Job
    {
        protected override async Task Update()
        {
            await this.WaitSeconds(0.5f);
            
            await new WaitForMoveDecisionJob().Run();
            await new MoveCurrentActorJob().Run();

            bool isLevelOver = await new GameOverCheckJob().Run();
            if (!isLevelOver) await new RunTurnJob().Run();
        }
    }
}