using System;
using System.Threading.Tasks;
using Core.Tools.Observables;

namespace Core.Tools
{
    public abstract class Job<TResult> : ILifetimeOwner
    {
        public Event<TResult> CompletedEvent { get; } = new Event<TResult>();
        public Lifetime Lifetime { get; internal set; }
        
        public virtual async Task<TResult> Run(Lifetime parentLifetime = null)
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
    
    public abstract class Job : Job<Void> { }
}