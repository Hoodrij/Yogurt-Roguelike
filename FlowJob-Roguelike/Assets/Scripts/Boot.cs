using System.Threading.Tasks;
using Core.Tools;
using Roguelike.Jobs;
using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            // await MyJob.Run();
            // MyJob myJob = new MyJob();
            // await myJob.CompletedEvent;
            // new RunGameJob().Run();
        }
    }
    
    public class MyJob : Job
    {
        protected override async Task<Void> Update()
        {
            return default;
        }
    }
}