using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using Entities.TurnSystem;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.Jobs
{
    public class MoveTurnOwnerJob : Job
    {
        protected override async Task Run()
        {
            AgentAspect agentAspect = Query.Of<AgentAspect>().With<TurnOwner>().Single();
            Direction direction = await agentAspect.Agent.MoveJob.Run(agentAspect);
            
            bool shouldMoveAtPosition = true;
            
            Vector2Int newPos = agentAspect.PhysBodyAspect.Position.Value + direction;
            foreach (Entity target in Physics.GetEntitiesAtPosition(newPos).ToList())
            {
                shouldMoveAtPosition &= await new InteractWithEntityJob().Run((agentAspect, target));
            }

            if (shouldMoveAtPosition)
            {
                agentAspect.PhysBodyAspect.Position.Value += direction;
                agentAspect.View.UpdateView(agentAspect);
            }
        }
    }
}