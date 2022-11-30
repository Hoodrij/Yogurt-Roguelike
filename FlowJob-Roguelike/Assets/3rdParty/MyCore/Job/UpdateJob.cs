﻿using System.Threading.Tasks;
using UnityAsync;

namespace Core.Tools
{
    public abstract class UpdateJob : Job
    {
        public override async Task<Void> Run(Void args, Lifetime parentLifetime)
        {
            using (Lifetime = new Lifetime(parentLifetime))
            {
                while (Lifetime.IsAlive)
                {
                    await this.WaitUpdate();
                    await Run(args);
                }
            }
            return default;
        }
    }
}