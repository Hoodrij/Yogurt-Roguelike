using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class GiveTurnToNextAgentJob : Job
    {
        public override void Run()
        {
            bool currentAgentFound = false;
            
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>())
            {
                if (agentAspect.Has<TurnOwner>())
                {
                    agentAspect.Remove<TurnOwner>();
                    currentAgentFound = true;
                } 
                else if (currentAgentFound)
                {
                    agentAspect.Add<TurnOwner>();
                    break;
                }
            }
            
            if (!Query.Of<TurnOwner>().Single().Exist)
            {
                Query.Of<Agent>().Single().Add<TurnOwner>();
            }
        }
    }
}