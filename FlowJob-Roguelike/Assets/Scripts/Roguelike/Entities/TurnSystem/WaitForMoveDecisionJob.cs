using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike;
using Roguelike.Entities;
using Roguelike.Jobs;

namespace Entities.TurnSystem
{
    public class WaitForMoveDecisionJob : Job
    {
        protected override async Task Update()
        {
            AgentAspect agentAspect = Aspect<CurrentAgentAspect>.Single().AgentAspect;
            
            Direction moveDecision = agentAspect.Type switch
            {
                AgentType.Player => await new GetPlayerInputJob().Run(),
                AgentType.Ai => Direction.Random,
            };
            
            agentAspect.Agent.MoveDecision = moveDecision;
        }
    }
}