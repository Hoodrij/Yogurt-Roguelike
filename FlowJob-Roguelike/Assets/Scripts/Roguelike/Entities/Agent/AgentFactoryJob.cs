﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike;
using Roguelike.Abilities;
using Roguelike.Entities;
using Tools;
using UnityEngine;
using Collider = Roguelike.Collider;

namespace Entities.TurnSystem
{
    public class AgentFactoryJob : Job<AgentAspect, AgentFactoryJob.Args> 
    {
        public struct Args
        {
            public Team Team;
            public Job<Void, AgentAspect> TurnJob;
            public List<Ability> Abilities;
            public Vector2Int Position;
            public CollisionLayer Layer;
            public CollisionLayer CanMoveAt;
            public Asset<AgentView> ViewRef;
        }
        
        protected override async Task<AgentAspect> Run(Args args)
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
            agentEntity.Add(view);
            view.UpdateView(agentEntity.ToAspect<AgentAspect>(), 0);
            
            return agentEntity.ToAspect<AgentAspect>();
        }
    }
}