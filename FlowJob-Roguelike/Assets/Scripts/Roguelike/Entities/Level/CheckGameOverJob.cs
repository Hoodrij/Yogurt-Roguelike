using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class CheckGameOverJob : Job<bool>
    {
        protected override async Task<bool> Run()
        {
            return Query.Single<GameAspect>().Health.Value <= 0;
        }
    }
}