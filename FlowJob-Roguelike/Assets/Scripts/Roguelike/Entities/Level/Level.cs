using FlowJob;

namespace Roguelike.Entities
{
    public class Level : IComponent
    {
        public static Entity Create()
        {
            Entity entity = Entity.Create();
            entity.SetParent(Query.With<Level>().Single());
            return entity;
        }
    }
}