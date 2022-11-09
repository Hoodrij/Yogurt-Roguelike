using System;
using Core.Tools.Observables;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public abstract class Job : ILifetimeOwner
    {
        public Event CompletedEvent { get; } = new Event();
        public Lifetime Lifetime { get; private set; }
    
        public async UniTask Run(Lifetime parentLifetime = null)
        {
            using (Lifetime = new Lifetime(parentLifetime))
            {
                await Run();
                CompletedEvent.Fire();
            }
        }
        
        protected abstract UniTask Run();
    
        public static Job As(Func<UniTask> action) => new AnonymousJob(action);
    }

    public abstract class Job<TResult> : ILifetimeOwner
    {
        public Event<TResult> CompletedEvent { get; } = new Event<TResult>();
        public Lifetime Lifetime { get; private set; }
        
        public async UniTask<TResult> Run(Lifetime parentLifetime = null)
        {
            using (Lifetime = new Lifetime(parentLifetime))
            {
                TResult result = await Run();
                CompletedEvent.Fire(result);
                return result;
            }
        }
        
        protected abstract UniTask<TResult> Run();

        public static Job<TResult> As(Func<UniTask<TResult>> action) => new AnonymousJob<TResult>(action);
    }
    
    
}