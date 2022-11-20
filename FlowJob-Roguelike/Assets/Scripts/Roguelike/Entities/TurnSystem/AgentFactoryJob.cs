using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using Roguelike.Entities;
using Roguelike.Jobs;
using UnityEngine;
using Collider = Roguelike.Entities.Collider;

namespace Entities.TurnSystem
{
    public class AgentFactoryJob : Job<Entity> 
    {
        protected override async Task<Entity> Update()
        {
            Data data = Query.Single<Data>();

            Vector2Int coord = (Vector2Int.one * 10).RandomTo();
            Entity agentEntity = Level.Create()
                .Add<Collider>()
                .Add(new Agent
                {
                    MoveJob = new GetPlayerInputJob()
                })
                .Add(new Position
                {
                    Coord = coord
                })
                .Add(new Health
                {
                    Value = data.StartingPlayerHealth
                });
            
            return agentEntity;
        }
    }
}