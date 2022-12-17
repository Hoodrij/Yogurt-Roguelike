using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class MoveTurnOwnerJob : Job
    {
        protected override async Task Run()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<TurnOwner>().Single();
            await agentAspect.Agent.TurnJob.Run(agentAspect);
            
            // bool shouldMoveAtPosition = true;
            //
            // Vector2Int newPos = agentAspect.PhysBodyAspect.Position.Value + direction;
            // foreach (Entity target in Physics.GetEntitiesAtPosition(newPos).Except(agentAspect.Entity).ToList())
            // {
            //     shouldMoveAtPosition &= await new InteractWithEntityJob().Run((agentAspect, target));
            // }
            //
            // if (shouldMoveAtPosition)
            // {
            //     agentAspect.PhysBodyAspect.Position.Value += direction;
            //     agentAspect.View.UpdateView(agentAspect);
            // }
        }
    }
}