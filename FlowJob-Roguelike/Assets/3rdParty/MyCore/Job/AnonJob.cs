using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    internal class AnonJob<TResult> : Job<TResult>
    {
        private readonly Func<Task<TResult>> action;
    
        internal AnonJob(Func<Task<TResult>> action)
        {
            this.action = action;
        }
    
        protected override async Task<TResult> Update()
        {
            return await action();
        }
    }
}