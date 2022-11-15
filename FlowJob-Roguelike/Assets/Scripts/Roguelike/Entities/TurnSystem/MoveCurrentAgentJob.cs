﻿using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class MoveCurrentAgentJob : Job
    {
        protected override async Task Update()
        {
            AgentAspect agentAspect = Aspect<CurrentAgentAspect>.Single().AgentAspect;
            Direction direction = await agentAspect.Agent.GetMoveJob.Run();
            agentAspect.Position.Coord += direction; 
        }
    }
}