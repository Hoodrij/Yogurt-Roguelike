using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class MoveTurnOwnerJob : Job
    {
        protected override async Task Run()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<TurnOwner>().Single();
            await agentAspect.Agent.MoveJob.Run(agentAspect);
        }
    }
}