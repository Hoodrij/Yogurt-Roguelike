using System;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    internal class AnonymousJob : Job
    {
        private readonly Func<UniTask> action;

        internal AnonymousJob(Func<UniTask> action)
        {
            this.action = action;
        }
    
        protected override async UniTask Run()
        {
            await action();
        }
    }
    
    internal class AnonymousJob<TResult> : Job<TResult>
    {
        private readonly Func<UniTask<TResult>> action;

        internal AnonymousJob(Func<UniTask<TResult>> action)
        {
            this.action = action;
        }
    
        protected override async UniTask<TResult> Run()
        {
            return await action();
        }
    }
}