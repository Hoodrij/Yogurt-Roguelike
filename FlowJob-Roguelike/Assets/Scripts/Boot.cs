using FlowJob;
using Roguelike.Jobs;
using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            WorldDebug wd = new WorldDebug();
            new RunGameJob().Run();

            // QueryOfEntity query = Query.Of<Player>();
            // Entity player = Query.Of<Player>().Single();
            // Player player1 = Query.Single<Player>();
            //
            // PlayerAspect playerAspect = Query.Single<PlayerAspect>();
            // PlayerAspect playerAspect1 = Query.Of<PlayerAspect>().Single();
        }
    }
}