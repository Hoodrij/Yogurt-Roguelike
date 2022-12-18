using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class MakeTurnJob : Job
    {
        public override async Task Run()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<TurnOwner>().Single();
            await agentAspect.Agent.TurnJob.Run(agentAspect);
        }
    }
}