using System;
using System.Threading.Tasks;

namespace Core.Tools 
{
    public abstract class Job<TResult, TParams> : ILifetimeOwner
    {
        public Lifetime Lifetime { get; private protected set; }

        public virtual async Task<TResult> Run(Lifetime parentLifetime = null, TParams args = default)
        {
            Job<TResult, TParams> job = (Job<TResult, TParams>) Activator.CreateInstance(GetType());
            using (job.Lifetime = new Lifetime(parentLifetime))
            {
                return await job.Run(args);
            }
        }

        protected abstract Task<TResult> Run(TParams args);
    }

    public abstract class Job : Job<Void, Void>
    {
        protected override async Task<Void> Run(Void args)
        {
            await Run();
            return default;
        }

        protected abstract Task Run();
    }
    
    public abstract class Job<TResult> : Job<TResult, Void>
    {
        protected override async Task<TResult> Run(Void args)
        {
            return await Run();
        }

        protected abstract Task<TResult> Run();
    }
}