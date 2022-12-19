namespace Core.Tools 
{
    public abstract class Job<TResult, TParams>
    {
        public abstract TResult Run(TParams args);
    }
    
    public abstract class Job
    {
        public abstract void Run();
    }
    
    public abstract class Job<TResult>
    {
        public abstract TResult Run();
    }
}