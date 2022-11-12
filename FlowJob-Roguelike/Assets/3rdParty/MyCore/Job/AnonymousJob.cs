using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    internal class AnonymousJob : Job
    {
        private readonly Func<Task> action;

        internal AnonymousJob(Func<Task> action)
        {
            this.action = action;
        }
    
        protected override async Task Run()
        {
            await action();
        }
    }
    
    internal class AnonymousJob<TResult> : Job<TResult>
    {
        private readonly Func<Task<TResult>> action;

        internal AnonymousJob(Func<Task<TResult>> action)
        {
            this.action = action;
        }
    
        protected override async Task<TResult> Run()
        {
            return await action();
        }
    }
}