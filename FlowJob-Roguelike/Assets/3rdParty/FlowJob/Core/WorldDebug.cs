using System.Collections.Generic;

namespace FlowJob
{
    public class WorldDebug : World.Accessor
    {
        public HashSet<Entity> Entities => this.GetEntities();
    }
}