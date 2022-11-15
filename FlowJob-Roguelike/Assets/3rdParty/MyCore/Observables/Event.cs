using System;
using System.Threading.Tasks;
using Core.Tools.Collections;

namespace Core.Tools.Observables
{
    public class Event : Event<Void> { }
    
    public class Event<T>
    {
        private readonly SafeHashSet<WeakAction<T>> listeners = new SafeHashSet<WeakAction<T>>();

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
        
        public EventAwaiter<T> GetAwaiter() => new EventAwaiter<T>(this);
        public static implicit operator Task<T>(Event<T> @event) => Task.Run(async () => await @event);
    }
}