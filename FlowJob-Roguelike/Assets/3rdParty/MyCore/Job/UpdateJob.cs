using System.Threading.Tasks;
using UnityAsync;

namespace Core.Tools
{
    public abstract class UpdateJob : ILifetimeOwner
    {
        public Lifetime Lifetime { get; private set; }
    
        public async Task Run(Lifetime parentLifetime)
        {
            using Lifetime _ = Lifetime = new Lifetime(parentLifetime);
            while (Lifetime.IsAlive)
            {
                await this.WaitUpdate();
                await Update();
            }
        }
        
        protected abstract Task Update();
    }
}