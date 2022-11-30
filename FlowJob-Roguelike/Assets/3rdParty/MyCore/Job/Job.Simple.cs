using System.Threading.Tasks;

namespace Core.Tools
{
    public abstract class Job : Job<Void, Void>
    {
        public override Task<Void> Run(Void args = default, Lifetime parentLifetime = null)
        {
            return base.Run(args, parentLifetime);
        }

        protected override async Task<Void> Run(Void args)
        {
            await Run();
            return default;
        }

        protected abstract Task Run();
    }
    
    public abstract class Job<TResult> : Job<TResult, Void>
    {
        public override Task<TResult> Run(Void args = default, Lifetime parentLifetime = null)
        {
            return base.Run(args, parentLifetime);
        }
        
        protected override async Task<TResult> Run(Void args)
        {
            return await Run();
        }

        protected abstract Task<TResult> Run();
    }
}