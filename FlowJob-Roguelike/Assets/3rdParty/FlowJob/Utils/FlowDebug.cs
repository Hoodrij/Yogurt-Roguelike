using System.Collections.Generic;
using System.Linq;

namespace FlowJob
{
    public class FlowDebug
    {
        public List<Entity> Entities => WorldAccessor.GetEntities().ToList();
    }
}