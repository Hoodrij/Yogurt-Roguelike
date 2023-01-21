using FlowJob;
using UnityEngine;
using UnityEngine.Profiling;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private TurnOwner turnOwner;
        
        private void Awake()
        {
            // new RunGameJob().Run();
        }

        private void Update()
        {
            Profiler.BeginSample("---Create");
            for (int i = 0; i < 100; i++)
            {
                Entity.Create()
                    .Add(turnOwner);
            }
            Profiler.EndSample();
            
            Profiler.BeginSample("---Iterate");
            foreach (Entity entity in Query.Of<TurnOwner>())
            {
                TurnOwner turnOwner = entity.Get<TurnOwner>();
            }
            Profiler.EndSample();
            
            Profiler.BeginSample("---Kill");
            foreach (Entity entity in Query.Of<TurnOwner>())
            {
                entity.Kill();
            }
            Profiler.EndSample();
        }
    }
}