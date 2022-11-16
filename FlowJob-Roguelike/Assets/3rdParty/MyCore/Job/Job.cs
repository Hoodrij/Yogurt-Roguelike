using System;
using System.Threading.Tasks;

namespace Core.Tools 
{
    public abstract class Job<TResult> : ILifetimeOwner
    {
        public Lifetime Lifetime { get; private protected set; }

        public virtual async Task<TResult> Run(Lifetime parentLifetime = null)
        {
            Job<TResult> job = (Job<TResult>) Activator.CreateInstance(GetType());
            using (job.Lifetime = new Lifetime(parentLifetime))
            {
                return await job.Update();
            }
        }

        protected abstract Task<TResult> Update();
    }
    
    public abstract class Job : Job<Void> { }
}