using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike
{
    public class MakeTurnJob : Job<UniTask>
    {
        public override async UniTask Run()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<TurnOwner>().Single();
            await agentAspect.Agent.TurnJob.Run(agentAspect);
        }
    }
}