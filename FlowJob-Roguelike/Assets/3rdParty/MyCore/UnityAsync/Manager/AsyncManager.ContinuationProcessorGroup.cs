using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityAsync
{
	public partial class AsyncManager
	{
		public partial class ContinuationProcessorGroup
		{
			const int InitialCapacity = 1 << 10;
			
			interface IContinuationProcessor
			{
				void Process();
			}

			readonly Dictionary<Type, IContinuationProcessor> processors;

			public ContinuationProcessorGroup()
			{
				processors = new Dictionary<Type, IContinuationProcessor>(16);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Add<T>(in T cont) where T : IAwaitInstructionAwaiter
			{
				if (!processors.TryGetValue(typeof(T), out var p))
				{
					p = ContinuationProcessor<T>.instance = new ContinuationProcessor<T>(InitialCapacity);
					processors.Add(typeof(T), ContinuationProcessor<T>.instance);
				}

				(p as ContinuationProcessor<T>)?.Add(cont);
			}

			public void Process()
			{
				for(int i = 0; i < processors.Count; ++i)
				{
					KeyValuePair<Type,IContinuationProcessor> pair = processors.ElementAt(i);
					pair.Value.Process();
				}
			}
		}
	}
}