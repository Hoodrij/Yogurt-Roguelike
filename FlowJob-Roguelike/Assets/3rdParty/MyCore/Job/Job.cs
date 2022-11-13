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
                await Update();
                CompletedEvent.Fire();
            }
        }
        
        protected abstract Task Update();
    
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
                TResult result = await Update();
                CompletedEvent.Fire(result);
                return result;
            }
        }
        
        protected abstract Task<TResult> Update();
    
        public static Job<TResult> As(Func<Task<TResult>> action) => new AnonymousJob<TResult>(action);
    }
    
    
}