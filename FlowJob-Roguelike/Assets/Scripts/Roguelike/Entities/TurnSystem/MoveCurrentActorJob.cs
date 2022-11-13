using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class MoveCurrentActorJob : Job
    {
        protected override async Task Update()
        {
            ActorAspect actorAspect = Aspect<CurrentActorAspect>.Single().ActorAspect;
            actorAspect.Position.Coord += actorAspect.Actor.MoveDecision;
        }
    }
}