using System.Threading.Tasks;

namespace Core.Tools 
{
    public abstract class Job<TResult, TParams>
    {
        public abstract Task<TResult> Run(TParams args);
    }
    
    public abstract class Job
    {
        public abstract Task Run();
    }
    
    public abstract class Job<TResult>
    {
        public abstract Task<TResult> Run();
    }
}