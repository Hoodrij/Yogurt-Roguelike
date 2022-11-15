using System;
using UnityEngine;

namespace Core.Tools.Observables
{
    internal class WeakAction<T>
    {
        public bool IsAlive => owner.IsAlive && owner?.Target != null && IsAliveAsMonoBeh();
        
        private readonly WeakReference owner;
        private readonly Action<T> callback;

        public WeakAction(Action<T> action, object owner = default)
        {
            this.owner = new WeakReference(owner ?? action.Target);
            callback = action;
        }

        public void Invoke(T t)
        {
            if (IsActiveAsMonoBeh())
                callback.Invoke(t);
        }

        public bool IsOwnedBy(object owner) => this.owner.Target == owner;
        
        private bool IsAliveAsMonoBeh() => owner.Target is not MonoBehaviour mono || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => owner.Target is not MonoBehaviour mono || mono.gameObject.activeInHierarchy;
    }
}