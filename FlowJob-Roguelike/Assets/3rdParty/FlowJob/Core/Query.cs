using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowJob
{
    public interface Query
    {
        internal Mask Included { get; set; }
        internal Mask Excluded { get; set; }

        public static CQuery Of<TComponent>() where TComponent : IComponent
        {
            return new CQuery().With<TComponent>();
        }
        
        public static AQuery<TAspect> Of<TAspect>(Void _ = default) where TAspect : struct, Aspect<TAspect>
        {
            return AspectCache.Get<TAspect>();
        }
        
        public static ref TComponent Single<TComponent>() where TComponent : IComponent
        {
            return ref new CQuery().With<TComponent>().Single().Get<TComponent>();
        }
        
        public static TAspect Single<TAspect>(Void _ = default) where TAspect : struct, Aspect<TAspect>
        {
            return new TAspect { Entity = ((Query)Of<TAspect>()).GetGroup().Single() };
        }

        protected internal Group GetGroup()
        {
            Composition composition = new Composition(Included, Excluded);
            Group group = Group.GetGroup(composition);
            return group;
        }
    }

    public struct CQuery : Query, IEnumerable<Entity>
    {
        Mask Query.Included { get; set; }
        Mask Query.Excluded { get; set; }
        private Query This => this;
        
        public CQuery With<TComponent>() where TComponent : IComponent
        {
            This.Included.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        public CQuery Without<TComponent>() where TComponent : IComponent
        {
            This.Excluded.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        public Entity Single() => This.GetGroup().Single();

        public IEnumerator<Entity> GetEnumerator() => This.GetGroup().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public struct AQuery<TAspect> : Query, IEnumerable<Aspect<TAspect>> where TAspect : struct, Aspect<TAspect>
    {
        Mask Query.Included { get; set; }
        Mask Query.Excluded { get; set; }
        private Query This => this;
        
        public AQuery<TAspect> With<TComponent>() where TComponent : IComponent
        {
            This.Included.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        public AQuery<TAspect> Without<TComponent>() where TComponent : IComponent
        {
            This.Excluded.Set(ComponentID.Of<TComponent>());
            return this;
        }
        
        public TAspect Single() => new TAspect { Entity = This.GetGroup().Single() };

        public IEnumerator<Aspect<TAspect>> GetEnumerator()
        {
            foreach (Entity entity in This.GetGroup())
            {
                TAspect aspect = new TAspect { Entity = entity };
                yield return aspect;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}