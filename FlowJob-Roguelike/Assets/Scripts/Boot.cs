using FlowJob;
using Roguelike.Jobs;
using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            new RunGameJob().Run();
        }

        private void Update()
        {
            WorldDebug worldDebug = new WorldDebug();
        }
    }
}