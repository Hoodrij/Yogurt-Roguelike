using System;
using System.Threading.Tasks;

namespace Core.Tools 
{
    public abstract class Job<TResult, TParams> : ILifetimeOwner
    {
        public Lifetime Lifetime { get; private protected set; }

        public virtual async Task<TResult> Run(TParams args, Lifetime parentLifetime = null)
        {
            Job<TResult, TParams> job = (Job<TResult, TParams>) Activator.CreateInstance(GetType());
            using (job.Lifetime = new Lifetime(parentLifetime))
            {
                return await job.Run(args);
            }
        }
 
        protected abstract Task<TResult> Run(TParams args);
    }
}