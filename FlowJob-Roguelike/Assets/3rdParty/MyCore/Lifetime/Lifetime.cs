﻿using System;
using System.Threading;

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
            await Wait.While(() => IsAlive);
            child.Kill();
        }

        public static implicit operator CancellationToken(Lifetime lifetime)
        {
            return lifetime.cts.Token;
        }
        
        public static implicit operator bool(Lifetime lifetime)
        {
            return lifetime.IsAlive;
        }

        public void Dispose()
        {
            Kill();
        }
    }
}