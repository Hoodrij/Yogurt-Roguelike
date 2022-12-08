using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using Entities.TurnSystem;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class EnemyTurnJob : Job<Void, AgentAspect>
    {
        protected override async Task<Void> Run(AgentAspect agentAspect)
        {
            Vector2Int position = agentAspect.PhysBodyAspect.Position.Value;
            // foreach (EntityAtDir entityAtDir in GetEntitiesAround(position))
            // {
            //     
            // }

            EntityAtDir dirToMoveAt = GetEntitiesAround(position)
                .Where(entityAtDir => CanMoveAt(agentAspect, entityAtDir.Entity))
                .GetRandom();
            
            await new AgentMoveJob().Run((agentAspect, dirToMoveAt.Direction));
            

            //
            // List<Direction> destructiblesAround 
            //     = Physics.GetDirectionsAround(enemyPos.Value, CollisionLayer.Destructible).ToList();
            // if (!destructiblesAround.IsEmpty())
            //     return destructiblesAround.GetRandom();
            //
            // List<Direction> freeDirectionsAround 
            //     = Physics.GetDirectionsAround2(enemyPos.Value, agentAspect.PhysBodyAspect.Collider.CanMoveAt).ToList();
            // if (!freeDirectionsAround.IsEmpty())
            //     return freeDirectionsAround.GetRandom();

            return default;
        }

        private static bool CanMoveAt(AgentAspect enemy, Entity other)
        {
            if (other == Entity.Null) return true;
            if (!other.TryGet(out Collider otherCollider)) return true;
            
            CollisionLayer enemyCanMoveAtLayer = enemy.PhysBodyAspect.Collider.CanMoveAt;
            CollisionLayer otherLayer = otherCollider.Layer;
            
            return enemyCanMoveAtLayer.HasFlag(otherLayer);
        }
        
        private static IEnumerable<EntityAtDir> GetEntitiesAround(Vector2Int origin)
        {
            foreach (Direction direction in Direction.All)
            {
                Vector2Int newPoint = origin + direction;
                bool hasEntity = false;
                foreach (Entity entity in Physics.GetEntitiesAtPosition(newPoint))
                {
                    hasEntity = true;
                    yield return new EntityAtDir
                    {
                        Direction = direction,
                        Entity = entity
                    };
                }

                if (!hasEntity)
                {
                    yield return new EntityAtDir
                    {
                        Direction = direction,
                        Entity = Entity.Null
                    };
                }
            }
        }

        private struct EntityAtDir
        {
            public Direction Direction;
            public Entity Entity;
        }
    }
}