using System.Collections.Generic;
using FlowJob;

namespace Roguelike.Entities
{
    public class Level : IComponent
    {
        public List<Entity> Agents = new();
        public int CurrentAgentIndex = -1;

        public static Entity Create()
        {
            Entity entity = Entity.Create();
            entity.SetParent(Query.Of<Level>().Single());
            return entity;
        }
    }
}