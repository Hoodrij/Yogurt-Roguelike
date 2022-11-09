using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public class Lifetime : IDisposable
    {
        public bool IsAlive { get; private set; } = true;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
            
        public Lifetime(Lifetime parentLifetime = null)
        {
            parentLifetime?.AddChild(this);
        }

        public void Kill()
        {
            IsAlive = false;
            cts.Cancel();
        }

        private async void AddChild(Lifetime child)
        {
            await UniTask.WaitWhile(() => IsAlive);
            child.Kill();
        }

        public static implicit operator CancellationToken(Lifetime lifetime)
        {
            return lifetime.cts.Token;
        }

        public void Dispose()
        {
            Kill();
        }
    }
}