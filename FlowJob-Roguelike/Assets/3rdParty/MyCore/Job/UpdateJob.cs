using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public abstract class UpdateJob : ILifetimeOwner
    {
        public Lifetime Lifetime { get; private set; }
    
        public async UniTask Run(Lifetime parentLifetime)
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