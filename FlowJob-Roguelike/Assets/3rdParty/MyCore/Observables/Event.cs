using System;
using Core.Tools.Collections;
using Cysharp.Threading.Tasks;

namespace Core.Tools.Observables
{
    public class Event
    {
        internal readonly SafeHashSet<WeakAction> listeners = new SafeHashSet<WeakAction>();

        public void Fire()
        {
            listeners.RemoveWhere(action => !action.IsAlive);
            listeners.ForEach(action => action.Invoke());
        }

        public void Listen(Action action, object owner = null) => listeners.Add(new WeakAction(action, owner));
        public void Unsubscribe(object owner) => listeners.RemoveWhere(weakAction => weakAction.IsOwnedBy(owner));
        public void Clear() => listeners.Clear();
        
        public static implicit operator UniTask(Event @event) => UniTask.RunOnThreadPool(async () => await @event);
    }

    public class Event<T>
    {
        internal readonly SafeHashSet<WeakAction<T>> listeners = new SafeHashSet<WeakAction<T>>();

        public void Fire(T t)
        {
            listeners.RemoveWhere(action => !action.IsAlive);
            listeners.ForEach(action => action.Invoke(t));
        }

        public void Listen(Action<T> action, object owner = null) => listeners.Add(new WeakAction<T>(action, owner));
        public void Unsubscribe(object owner)
        {
            listeners.RemoveWhere(weakAction => weakAction.IsOwnedBy(owner));
        }

        public void Clear() => listeners.Clear();
        
        public static implicit operator UniTask<T>(Event<T> @event) => UniTask.RunOnThreadPool(async () => await @event);
    }
}