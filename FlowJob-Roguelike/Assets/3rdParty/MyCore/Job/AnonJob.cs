using System;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    internal class AnonJob<TResult> : Job<TResult>
    {
        private readonly Func<UniTask<TResult>> action;
    
        internal AnonJob(Func<UniTask<TResult>> action)
        {
            this.action = action;
        }
    
        protected override async UniTask<TResult> Update()
        {
            return await action();
        }
    }
}