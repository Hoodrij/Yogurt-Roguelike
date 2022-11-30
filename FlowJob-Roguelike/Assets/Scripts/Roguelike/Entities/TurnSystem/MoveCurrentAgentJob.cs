using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class MoveCurrentAgentJob : Job
    {
        protected override async Task Run()
        {
            AgentAspect agentAspect = Query.Single<CurrentAgentAspect>().AgentAspect;
            Direction direction = await agentAspect.Agent.MoveJob.Run();
            
            
            agentAspect.PhysBodyAspect.Position.Coord += direction;
            agentAspect.View.UpdateView(agentAspect);
        }
    }
}