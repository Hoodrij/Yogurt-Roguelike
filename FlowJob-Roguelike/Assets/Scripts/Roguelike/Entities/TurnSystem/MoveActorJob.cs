using System.Threading.Tasks;
using Core.Tools;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class MoveActorJob : Job
    {
        private ActorAspect actorAspect;

        public MoveActorJob(ActorAspect actorAspect)
        {
            this.actorAspect = actorAspect;
        }

        protected override async Task Update()
        {
            actorAspect.Position.Coord += actorAspect.Actor.MoveDecision;
        }
    }
}