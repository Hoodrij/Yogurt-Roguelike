using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public abstract class UpdateJob : Job
    {
        public new async UniTask<Void> Run(Lifetime parentLifetime)
        {
            using (Lifetime = new Lifetime(parentLifetime))
            {
                while (Lifetime.IsAlive)
                {
                    await this.WaitUpdate();
                    await Update();
                }
            }
            return default;
        }
    }
}