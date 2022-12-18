using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class CheckGameOverJob : Job<bool>
    {
        public override async Task<bool> Run()
        {
            return Query.Single<GameAspect>().Health.Value <= 0;
        }
    }
}