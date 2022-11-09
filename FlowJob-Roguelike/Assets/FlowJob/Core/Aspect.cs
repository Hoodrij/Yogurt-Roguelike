﻿namespace FlowJob
{
    public interface Aspect
    {
        public Entity Entity { get; set; }
    }
    public interface Aspect<TAspect> : Aspect where TAspect : struct, Aspect<TAspect>
    {
        public static Query<TAspect> Query()
        {
            return AspectCache.Get<TAspect>();
        }
        
        public static TAspect Single()
        {
            return new TAspect
            {
                Entity = Query().Single()
            };
        }
    }

    public static class AspectEx
    {
        public static TAspect GetAspect<TAspect>(this Aspect aspect) where TAspect : struct, Aspect<TAspect>
        {
            return new TAspect
            {
                Entity = aspect.Entity
            };
        }

        public static bool Exist(this Aspect aspect) => aspect.Entity.Exist;
        
        public static ref TComponent Get<TComponent>(this Aspect aspect) where TComponent : IComponent, new() => ref aspect.Entity.Get<TComponent>();
        public static void Add<TComponent>(this Aspect aspect) where TComponent : IComponent, new() => aspect.Entity.Add<TComponent>();
        public static void Set<TComponent>(this Aspect aspect, TComponent component) where TComponent : IComponent => aspect.Entity.Set(component);
        public static bool Has<TComponent>(this Aspect aspect) where TComponent : IComponent => aspect.Entity.Has<TComponent>();
        public static void Remove<TComponent>(this Aspect aspect) where TComponent : IComponent => aspect.Entity.Remove<TComponent>();
        
        public static string Name(this Aspect aspect)
        {
            return $"{aspect.GetType()}_{aspect.Entity}";
        }
    }
}