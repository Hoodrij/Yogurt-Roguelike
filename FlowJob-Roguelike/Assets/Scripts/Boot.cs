using System;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        public struct Test : IUnmanaged<Test>
        {
            public int i;

            public void Initialize()
            {
                i = 0;
            }
            public void Dispose() { }

            public bool Equals(Test other)
            {
                return i == other.i;
            }
        }

        // private int i;
        private UnsafeSpan<Test> span;
            
        private void Awake()
        {
            // span = new(8);
            // span.Set(5, new Test {i = 10});
            //
            // span.Add(new Test {i = 100});
            // span.Set(7, new Test {i = 100});
            // span.Remove(new Test {i = 10});
            //
            // while (span.Count > 0)
            // {
            //     span.Remove(*span.Get(0));
            // }
            //
            // for (int i = 0; i < span.Count; i++)
            // {
            //     Test* test = span.Get(i);
            //     Debug.Log(test->i);
            // }

            new RunGameJob().Run();
        }
    }
}