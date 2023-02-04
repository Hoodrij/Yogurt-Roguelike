namespace Yogurt.Roguelike
{
    public struct CheckGameOverJob
    {
        public bool Run()
        {
            return Query.Single<GameAspect>().Health.Value <= 0;
        }
    }
}