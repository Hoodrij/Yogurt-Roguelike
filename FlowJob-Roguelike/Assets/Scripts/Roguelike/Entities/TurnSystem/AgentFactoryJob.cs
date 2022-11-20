using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;
using Collider = Roguelike.Entities.Collider;

namespace Entities.TurnSystem
{
    public class AgentFactoryJob : Job<AgentAspect> 
    {
        protected override async Task<AgentAspect> Update()
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