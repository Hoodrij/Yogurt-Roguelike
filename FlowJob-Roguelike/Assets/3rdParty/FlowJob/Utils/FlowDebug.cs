using System.Collections.Generic;
using System.Linq;

namespace FlowJob
{
    public class FlowDebug : World.Accessor
    {
        public List<Entity> Entities => this.GetEntities().ToList();
    }
}