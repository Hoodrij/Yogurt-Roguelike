using System.Collections.Generic;
using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using FlowJob;
using Roguelike.Abilities;
using Roguelike.Tools;
using UnityEngine;

namespace Roguelike
{
    public struct AgentFactoryJob
    {
        public struct Args
        {
            public Team Team;
            public Agent.ITurnJob TurnJob;
            public List<IAbility> Abilities;
            public Vector2Int Position;
            public CollisionLayer Layer;
            public CollisionLayer CanMoveAt;
            public Asset<AgentView> ViewRef;
        }
        
        public async UniTask<AgentAspect> Run(Args args)
        {
            Entity agentEntity = Level.Create()
                .Add(new Collider
                {
                    Layer = args.Layer,
                    CanMoveAt = args.CanMoveAt
                })
                .Add(new Agent
                {
                    Team = args.Team,
                    TurnJob = args.TurnJob,
                    Abilities = args.Abilities,
                })
                .Add(new Position
                {
                    Value = args.Position
                })
                .Add(new Health());
            
            AgentView view = await args.ViewRef.Spawn();
            agentEntity.AddForLife(view);
            view.UpdateView(agentEntity.ToAspect<AgentAspect>(), 0);
            
            return agentEntity.ToAspect<AgentAspect>();
        }
    }
}