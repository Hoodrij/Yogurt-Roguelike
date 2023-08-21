namespace Yogurt.Roguelike
{
    public struct GiveTurnToNextAgentJob
    {
        public void Run()
        {
            bool currentAgentFound = false;
            
            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>())
            {
                if (agentAspect.Has<TurnOwner>())
                {
                    agentAspect.Remove<TurnOwner>();
                    currentAgentFound = true;
                    continue;
                } 
                
                if (currentAgentFound)
                {
                    agentAspect.Add(new TurnOwner());
                    break;
                }
            }
            
            if (!Query.Of<TurnOwner>().Single().Exist)
            {
                Query.Of<Agent>().Single().Add(new TurnOwner());
            }
        }
    }
}