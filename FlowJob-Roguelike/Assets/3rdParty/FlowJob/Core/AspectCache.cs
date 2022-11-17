using System;
using System.Collections.Generic;
using System.Reflection;

namespace FlowJob
{
    public static class AspectCache
    {
        private static Dictionary<Type, Mask> cache = new();
        
        public static QueryOfAspect<TAspect> Get<TAspect>() where TAspect : struct, Aspect<TAspect>
        {
            QueryOfAspect<TAspect> query = new QueryOfAspect<TAspect>();
            Type aspectType = typeof(TAspect);

            if (!cache.TryGetValue(aspectType, out Mask mask))
            {
                mask = GenerateMask(aspectType);
                cache.Add(aspectType, mask);
            }

            ((Query)query).Included = mask;
            return query;
        }
        
        private static Mask GenerateMask(Type aspectType)
        {
            Mask mask = default;
            foreach (PropertyInfo field in aspectType.GetProperties())
            {
                Type propertyType = field.PropertyType;
                if (propertyType.GetInterface(nameof(IComponent)) != null)
                {
                    mask.Set(ComponentID.Of(propertyType));
                }
                    
                if (propertyType.GetInterface(nameof(Aspect)) != null)
                {
                    mask |= GenerateMask(propertyType);
                }
            }

            return mask;
        }

        public static void Clear()
        {
            cache.Clear();
        }
    }
}