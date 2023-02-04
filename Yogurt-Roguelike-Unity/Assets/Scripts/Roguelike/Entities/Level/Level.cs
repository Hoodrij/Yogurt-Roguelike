using Yogurt;

namespace Roguelike
{
    public class Level : IComponent
    {
        public static Entity Create()
        {
            Entity entity = Entity.Create();
            Entity parentEntity = Query.Of<Level>().Single();
            entity.SetParent(parentEntity);
            return entity;
        }
    }
}