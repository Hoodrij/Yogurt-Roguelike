using FlowJob;
using UnityEngine;
using UnityEngine.Profiling;

namespace Roguelike
{
    public class BootProfiler : MonoBehaviour
    {
        public int SamplesCount; 
        private TurnOwner turnOwner;
        
        private void Update()
        {
            Profiler.BeginSample("---Create");
            Entity parent = Entity.Create();
            for (int i = 0; i < SamplesCount; i++)
            {
                Entity.Create()
                    // .Add<TurnOwner>()
                    .Add(turnOwner)
                    // .SetParent(parent)
                    ;
            }
            Profiler.EndSample();
            
            Profiler.BeginSample("---Iterate");
            foreach (Entity entity in Query.Of<TurnOwner>())
            {
                entity.Get<TurnOwner>();
            }
            Profiler.EndSample();
            
            Profiler.BeginSample("---Kill");
            // parent.Kill();
            foreach (Entity entity in Query.Of<TurnOwner>())
            {
                entity.Kill();
            }
            Profiler.EndSample();
        }
    }
}