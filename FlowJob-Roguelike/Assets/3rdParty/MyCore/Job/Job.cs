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
                return await job.Update(args);
            }
        }

        protected abstract Task<TResult> Update(TParams args);
    }

    public abstract class Job : Job<Void, Void>
    {
        protected override async Task<Void> Update(Void args)
        {
            await Update();
            return default;
        }

        protected abstract Task Update();
    }
    
    public abstract class Job<TResult> : Job<TResult, Void>
    {
        protected override async Task<TResult> Update(Void args)
        {
            return await Update();
        }

        protected abstract Task<TResult> Update();
    }
}