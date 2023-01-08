using FlowJob;

namespace Roguelike
{
    public class CheckGameOverJob
    {
        public bool Run()
        {
            return Query.Single<GameAspect>().Health.Value <= 0;
        }
    }
}