using System.Collections;
using System.Collections.Generic;

namespace FlowJob
{
    public struct Query : IEnumerable<Entity>
    {
        private Mask included;
        private Mask excluded;

        public Entity Single()
        {
            return GetGroup().Single();
        }

        public static ref TComponent Single<TComponent>() where TComponent : IComponent
        {
            return ref new Query().With<TComponent>().Single().Get<TComponent>();
        }
        
        public static Query With<TComponent>(bool _ = false) where TComponent : IComponent
        {
            return new Query().With<TComponent>();
        }

        public Query With<TComponent>() where TComponent : IComponent
        {
            included.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        public Query Without<TComponent>() where TComponent : IComponent
        {
            excluded.Set(ComponentID.Of<TComponent>());
            return this;
        }

        private Group GetGroup()
        {
            Composition composition = new Composition(included, excluded);
            Group group = Group.GetGroup(composition);
            return group;
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return GetGroup().GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct Query<TAspect> : IEnumerable<TAspect> where TAspect : struct, Aspect<TAspect>
    {
        internal Mask included;
        internal Mask excluded;
        
        public Entity Single()
        {
            return GetGroup().Single();
        }

        public Query<TAspect> With<TComponent>() where TComponent : IComponent
        {
            included.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        public Query<TAspect> Without<TComponent>() where TComponent : IComponent
        {
            excluded.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        private Group GetGroup()
        {
            Composition composition = new Composition(included, excluded);
            Group group = Group.GetGroup(composition);
            return group;
        }
        
        public IEnumerator<TAspect> GetEnumerator()
        {
            foreach (Entity entity in GetGroup())
            {
                TAspect aspect = new TAspect { Entity = entity };
                yield return aspect;
            }
        }
    
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}