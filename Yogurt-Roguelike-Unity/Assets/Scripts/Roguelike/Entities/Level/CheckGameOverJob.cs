using Yogurt;

namespace Roguelike
{
    public struct CheckGameOverJob
    {
        public bool Run()
        {
            return Query.Single<GameAspect>().Health.Value <= 0;
        }
    }
}