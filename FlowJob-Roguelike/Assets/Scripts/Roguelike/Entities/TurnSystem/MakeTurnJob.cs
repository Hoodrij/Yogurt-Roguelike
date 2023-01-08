using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class MakeTurnJob
    {
        public async UniTask Run()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<TurnOwner>().Single();
            await agentAspect.Agent.TurnJob.Run(agentAspect);
        }
    }
}