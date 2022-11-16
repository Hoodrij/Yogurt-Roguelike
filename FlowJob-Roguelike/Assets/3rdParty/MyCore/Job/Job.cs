using System;
using System.Threading.Tasks;

namespace Core.Tools 
{
    public abstract class Job<TResult> : ILifetimeOwner
    {
        Lifetime ILifetimeOwner.Lifetime => Lifetime;
        private protected Lifetime Lifetime;

        public virtual async Task<TResult> Run(Lifetime parentLifetime = null)
        {
            Job<TResult> job = ((Job<TResult>) Activator.CreateInstance(GetType()));
            using (job.Lifetime = new Lifetime(parentLifetime))
            {
                return await job.Update();
            }
        }

        protected abstract Task<TResult> Update();
    }
    
    public abstract class Job : Job<Void> { }
}