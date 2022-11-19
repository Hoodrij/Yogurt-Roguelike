using System.Threading.Tasks;
using Core.Tools;
using FlowJob;
using Roguelike.Entities;

namespace Roguelike.Jobs
{
    public class GiveTurnToNextAgentJob : Job
    {
        protected override async Task<Void> Update()
        {
            Level level = Query.Single<Level>();

            foreach (Entity entity in Query.Of<CurrentTurnAgent>())
            {
                entity.Remove<CurrentTurnAgent>();
            }

            level.CurrentAgentIndex++;
            level.CurrentAgentIndex %= level.Agents.Count;
            
            level.Agents[level.CurrentAgentIndex].Add<CurrentTurnAgent>();

            return default;
        }
    }
}