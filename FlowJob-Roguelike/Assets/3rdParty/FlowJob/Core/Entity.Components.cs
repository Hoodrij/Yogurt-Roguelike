﻿using System;
using System.Linq;

namespace FlowJob
{
    public unsafe partial struct Entity : World.Accessor
    {
        internal EntityMeta* Meta => this.GetMeta(ID);

        public Entity Add<T>() where T : IComponent, new()
        {
            this.DebugCheckAlive();
            this.DebugAlreadyHave<T>();
            
            if (Has<T>()) return this;
            
            Set(new T());
            return this;
        }
        
        public Entity Add<T>(T component) where T : IComponent
        {
            Set(component);
            return this;
        }

        public Entity Set<T>(T component) where T : IComponent
        {
            this.DebugCheckAlive();
            
            if (Has<T>())
                Remove<T>();
            
            ComponentID componentID = ComponentID.Of<T>();
            Meta->ComponentsMask.Set(componentID);
            this.Enqueue(PostProcessor.Action.ComponentsChanged, this, componentID);
            Storage<T>.Instance.Add(component, ID);

            return this;
        }

        public ref T Get<T>() where T : IComponent
        {
            this.DebugCheckAlive();
            this.DebugNoComponent<T>();

            return ref Storage<T>.Instance.Get(ID);
        }
        
        public bool TryGet<T>(out T t) where T : IComponent
        {
            bool has = Has<T>();
            t = default;
            if (has)
            {
                t = Storage<T>.Instance.Get(ID);
            }

            return has;
        }

        public bool Has<T>() where T : IComponent
        {
            this.DebugCheckAlive();

            return Meta->ComponentsMask.Has(ComponentID.Of<T>());
        }

        public void Remove<T>() where T : IComponent
        {
            if (!Has<T>()) return;

            ComponentID componentID = ComponentID.Of<T>();
            Meta->ComponentsMask.UnSet(componentID);
            this.Enqueue(PostProcessor.Action.ComponentsChanged, this, componentID);
        }

        public void Kill()
        {
            this.DebugCheckAlive();
            this.Enqueue(PostProcessor.Action.Kill, this);

            foreach (IComponent component in this.GetComponents())
            {
                if (component is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            Meta->IsAlive = false;
            while (Managed.Childs.Count > 0)
            {
                Managed.Childs.First().Kill();
            }

            UnParent();
        }
    }
}