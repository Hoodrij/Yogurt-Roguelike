using System.Collections.Generic;
using System.Linq;

namespace FlowJob
{
    public class WorldDebug : World.Accessor
    {
        public List<Entity> Entities => this.GetEntities().Where(e => e.Alive).ToList();
    }
}