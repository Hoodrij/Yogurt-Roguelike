using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class GiveTurnToNextAgentJob : Job
    {
        protected override async Task Run()
        {
            bool currentAgentFound = false;
            
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>())
            {
                if (agentAspect.Has<CurrentAgentTag>())
                {
                    agentAspect.Remove<CurrentAgentTag>();
                    currentAgentFound = true;
                } 
                else if (currentAgentFound)
                {
                    agentAspect.Add<CurrentAgentTag>();
                    break;
                }
            }
            
            if (!Query.Single<CurrentAgentAspect>().Exist())
            {
                Query.Of<Agent>().Single().Add<CurrentAgentTag>();
            }
        }
    }
}