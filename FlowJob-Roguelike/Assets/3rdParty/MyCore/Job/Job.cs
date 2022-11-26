using System;
using Cysharp.Threading.Tasks;

namespace Core.Tools 
{
    public abstract class Job<TResult> : ILifetimeOwner
    {
        public Lifetime Lifetime { get; private protected set; }

        public async UniTask<TResult> Run(Lifetime parentLifetime = null)
        {
            Job<TResult> job = (Job<TResult>) Activator.CreateInstance(GetType());
            using (job.Lifetime = new Lifetime(parentLifetime))
            {
                return await job.Update();
            }
        }

        protected abstract UniTask<TResult> Update();

        public static Job<TResult> As(Func<UniTask<TResult>> action) => new AnonJob<TResult>(action);
    }
    
    public abstract class Job : Job<Void> { }
}