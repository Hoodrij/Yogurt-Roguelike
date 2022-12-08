using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class GetEnemyMoveDirectionJob : Job<Direction, AgentAspect>
    {
        protected override async Task<Direction> Run(AgentAspect agentAspect)
        {
            Vector2Int position = agentAspect.PhysBodyAspect.Position.Value;
            IEnumerable<EntityAtDir> entityAtDirs = GetEntitiesAround(position).ToList();
            
            IEnumerable<EntityAtDir> attackTargets = entityAtDirs.Where(entityAtDir => CanAttack(agentAspect, entityAtDir.Entity)).ToList();
            IEnumerable<EntityAtDir> moveTargets = entityAtDirs.Where(entityAtDir => CanMoveAt(agentAspect, entityAtDir.Entity)).ToList();
            
            EntityAtDir dirToMoveAt = !attackTargets.IsEmpty()
                ? attackTargets.GetRandom()
                : moveTargets.GetRandom();

            return dirToMoveAt.Direction;
        }
        
        private static bool CanAttack(AgentAspect enemy, Entity other)
        {
            if (!other.Exist) return false;
            if (!other.TryGet(out Collider otherCollider)) return false;

            CollisionLayer enemyAttackLayer = CollisionLayer.Destructible;
            CollisionLayer otherLayer = otherCollider.Layer;

            bool layerMatches = enemyAttackLayer.HasFlag(otherLayer);
            if (!layerMatches) return false;

            if (!other.TryGet(out Agent otherAgent)) return false;
            if (otherAgent.Team == enemy.Agent.Team) return false;

            return true;
        }

        private static bool CanMoveAt(AgentAspect enemy, Entity other)
        {
            if (!other.Exist) return true;
            if (!other.TryGet(out Collider otherCollider)) return true;
            
            CollisionLayer enemyCanMoveAtLayer = enemy.PhysBodyAspect.Collider.CanMoveAt;
            CollisionLayer otherLayer = otherCollider.Layer;
            
            bool layerMatches = enemyCanMoveAtLayer.HasFlag(otherLayer);
            if (!layerMatches) return false;

            if (!other.TryGet(out Agent otherAgent)) return true;
            if (otherAgent.Team == enemy.Agent.Team) return false;

            return true;
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