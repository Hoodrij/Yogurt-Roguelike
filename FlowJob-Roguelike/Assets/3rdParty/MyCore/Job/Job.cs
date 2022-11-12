using System;
using System.Threading.Tasks;
using Core.Tools.Observables;

namespace Core.Tools
{
    public abstract class Job : ILifetimeOwner
    {
        public Event CompletedEvent { get; } = new Event();
        public Lifetime Lifetime { get; private set; }
    
        public async Task Run(Lifetime parentLifetime = null)
        {
            using (Lifetime = new Lifetime(parentLifetime))
            {
                await Run();
                CompletedEvent.Fire();
            }
        }
        
        protected abstract Task Run();
    
        public static Job As(Func<Task> action) => new AnonymousJob(action);
    }

    public abstract class Job<TResult> : ILifetimeOwner
    {
        public Event<TResult> CompletedEvent { get; } = new Event<TResult>();
        public Lifetime Lifetime { get; private set; }
        
        public async Task<TResult> Run(Lifetime parentLifetime = null)
        {
            using (Lifetime = new Lifetime(parentLifetime))
            {
                TResult result = await Run();
                CompletedEvent.Fire(result);
                return result;
            }
        }
        
        protected abstract Task<TResult> Run();

        public static Job<TResult> As(Func<Task<TResult>> action) => new AnonymousJob<TResult>(action);
    }
    
    
}