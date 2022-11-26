using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class MoveCurrentAgentJob : Job
    {
        protected override async UniTask<Void> Update()
        {
            AgentAspect agentAspect = Query.Single<CurrentAgentAspect>().AgentAspect;
            Direction direction = await agentAspect.Agent.MoveJob.Run();
            
            agentAspect.PhysBodyAspect.Position.Coord += direction;
            agentAspect.View.UpdateView(agentAspect);

            return default;
        }
    }
}