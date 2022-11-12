using System;
using System.Threading.Tasks;
using Core.Tools.Collections;

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

        public static implicit operator Task(Event @event) => Task.Run(async () => await @event);
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
        
        public static implicit operator Task<T>(Event<T> @event) => Task.Run(async () => await @event);
    }
}