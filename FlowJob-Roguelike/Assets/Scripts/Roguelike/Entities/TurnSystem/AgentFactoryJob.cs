using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike;
using Roguelike.Entities;
using UnityEngine;
using Collider = Roguelike.Collider;

namespace Entities.TurnSystem
{
    public class AgentFactoryJob : Job<AgentAspect, AgentFactoryJob.Args> 
    {
        public struct Args
        {
            public Job<Void, AgentAspect> MoveJob;
            public Vector2Int Position;
            public CollisionLayer Layer;
            public CollisionLayer CollisionMap;
        }
        
        protected override async Task<AgentAspect> Run(Args args)
        {
            Entity agentEntity = Level.Create()
                .Add(new Collider
                {
                    Layer = args.Layer,
                    CollisionMap = args.CollisionMap
                })
                .Add(new Agent
                {
                    MoveJob = args.MoveJob
                })
                .Add(new Position
                {
                    Coord = args.Position
                })
                .Add(new Health());
            
            return agentEntity.ToAspect<AgentAspect>();
        }
    }
}