using FlowJob;
using Roguelike.Entities;
using Roguelike.Jobs;
using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            new RunGameJob().Run();

            CQuery cQuery = Query.Of<Player>();
            Player player = Query.Single<Player>();
            Entity player1 = Query.Of<Player>().Single();

            PlayerAspect playerAspect = Query.Single<PlayerAspect>();
            PlayerAspect playerAspect1 = Query.Of<PlayerAspect>().Single();

            playerAspect.Alive().log();
        }
    }
}