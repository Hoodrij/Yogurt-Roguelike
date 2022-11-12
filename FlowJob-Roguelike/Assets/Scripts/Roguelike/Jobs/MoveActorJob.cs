using Core.Tools;
using Cysharp.Threading.Tasks;
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

        protected override async UniTask Run()
        {
            actorAspect.Position.Coord += actorAspect.Actor.MoveDecision;
        }
    }
}