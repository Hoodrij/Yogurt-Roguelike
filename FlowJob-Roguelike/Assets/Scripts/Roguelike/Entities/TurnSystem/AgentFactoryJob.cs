using Core.Tools;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike;
using Roguelike.Entities;

namespace Entities.TurnSystem
{
    public class AgentFactoryJob : Job<AgentAspect> 
    {
        protected override async UniTask<AgentAspect> Update()
        {
            Entity agentEntity = Level.Create()
                .Add<Collider>()
                .Add(new Agent())
                .Add(new Position())
                .Add(new Health());
            
            return agentEntity.ToAspect<AgentAspect>();
        }
    }
}